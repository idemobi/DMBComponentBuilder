(function () {
    "use strict";

    var csharpKeywords = new Set([
        "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked",
        "class", "const", "continue", "decimal", "default", "delegate", "do", "double", "else",
        "enum", "event", "explicit", "extern", "false", "finally", "fixed", "float", "for",
        "foreach", "get", "global", "goto", "if", "implicit", "in", "init", "int", "interface",
        "internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator",
        "out", "override", "params", "private", "protected", "public", "readonly", "record",
        "ref", "return", "sbyte", "sealed", "set", "short", "sizeof", "stackalloc", "static",
        "string", "struct", "switch", "this", "throw", "true", "try", "typeof", "uint",
        "ulong", "unchecked", "unsafe", "ushort", "using", "var", "virtual", "void", "volatile",
        "while", "with", "yield"
    ]);

    var csharpTypes = new Set([
        "Action", "Array", "DateTime", "DateTimeOffset", "Dictionary", "Func", "Guid", "HashSet",
        "IEnumerable", "IList", "IReadOnlyCollection", "IReadOnlyDictionary", "IReadOnlyList",
        "List", "Math", "String", "Task", "ValueTask"
    ]);

    var cssAtRules = new Set([
        "@charset", "@container", "@font-face", "@import", "@keyframes", "@layer", "@media",
        "@namespace", "@page", "@property", "@supports"
    ]);

    function escapeHtml(value) {
        return value
            .replace(/&/g, "&amp;")
            .replace(/</g, "&lt;")
            .replace(/>/g, "&gt;")
            .replace(/"/g, "&quot;")
            .replace(/'/g, "&#39;");
    }

    function token(kind, value) {
        return "<span class=\"dmb-code-token-" + kind + "\">" + escapeHtml(value) + "</span>";
    }

    function isIdentifierStart(character) {
        return /[A-Za-z_@]/.test(character);
    }

    function isIdentifierPart(character) {
        return /[A-Za-z0-9_@]/.test(character);
    }

    function readQuoted(source, index, quote) {
        var current = index + 1;
        while (current < source.length) {
            if (source[current] === "\\") {
                current += 2;
                continue;
            }

            if (source[current] === quote) {
                return current + 1;
            }

            current++;
        }

        return source.length;
    }

    function readLineComment(source, index) {
        var end = source.indexOf("\n", index);
        return end === -1 ? source.length : end;
    }

    function readBlockComment(source, index) {
        var end = source.indexOf("*/", index + 2);
        return end === -1 ? source.length : end + 2;
    }

    function highlightCSharp(source) {
        var html = "";
        var index = 0;

        while (index < source.length) {
            var current = source[index];
            var next = source[index + 1] || "";

            if (current === "/" && next === "/") {
                var lineCommentEnd = readLineComment(source, index);
                html += token("comment", source.slice(index, lineCommentEnd));
                index = lineCommentEnd;
                continue;
            }

            if (current === "/" && next === "*") {
                var blockCommentEnd = readBlockComment(source, index);
                html += token("comment", source.slice(index, blockCommentEnd));
                index = blockCommentEnd;
                continue;
            }

            if ((current === "@" && next === "\"") || current === "$" && next === "\"") {
                var interpolatedEnd = readQuoted(source, index + 1, "\"");
                html += token("string", source.slice(index, interpolatedEnd));
                index = interpolatedEnd;
                continue;
            }

            if (current === "\"" || current === "'") {
                var quotedEnd = readQuoted(source, index, current);
                html += token("string", source.slice(index, quotedEnd));
                index = quotedEnd;
                continue;
            }

            if (/\d/.test(current)) {
                var numberMatch = source.slice(index).match(/^\d[\d_]*(\.\d[\d_]*)?([eE][+-]?\d+)?[fFdDmMuUlL]*/);
                html += token("number", numberMatch[0]);
                index += numberMatch[0].length;
                continue;
            }

            if (isIdentifierStart(current)) {
                var start = index;
                index++;
                while (index < source.length && isIdentifierPart(source[index])) {
                    index++;
                }

                var word = source.slice(start, index);
                var cleanWord = word.charAt(0) === "@" ? word.slice(1) : word;
                if (csharpKeywords.has(cleanWord)) {
                    html += token(cleanWord === "true" || cleanWord === "false" || cleanWord === "null" ? "boolean" : "keyword", word);
                } else if (csharpTypes.has(cleanWord) || /^[A-Z][A-Za-z0-9_]*$/.test(cleanWord)) {
                    html += token("type", word);
                } else {
                    html += escapeHtml(word);
                }
                continue;
            }

            if (/[{}()[\];,.<>:+\-*/=%!&|?]/.test(current)) {
                html += token(/[+\-*/=%!&|?]/.test(current) ? "operator" : "punctuation", current);
                index++;
                continue;
            }

            html += escapeHtml(current);
            index++;
        }

        return html;
    }

    function highlightJson(source) {
        var html = "";
        var index = 0;

        while (index < source.length) {
            var current = source[index];

            if (current === "\"") {
                var end = readQuoted(source, index, "\"");
                var raw = source.slice(index, end);
                var nextNonWhitespace = source.slice(end).match(/^\s*:/);
                html += token(nextNonWhitespace ? "property" : "string", raw);
                index = end;
                continue;
            }

            var literal = source.slice(index).match(/^(true|false|null)\b/);
            if (literal) {
                html += token("boolean", literal[0]);
                index += literal[0].length;
                continue;
            }

            var number = source.slice(index).match(/^-?\d+(\.\d+)?([eE][+-]?\d+)?/);
            if (number) {
                html += token("number", number[0]);
                index += number[0].length;
                continue;
            }

            if (/[{}[\]:,]/.test(current)) {
                html += token("punctuation", current);
                index++;
                continue;
            }

            html += escapeHtml(current);
            index++;
        }

        return html;
    }

    function highlightMarkup(source) {
        var html = "";
        var index = 0;

        while (index < source.length) {
            var current = source[index];

            if (current === "<") {
                if (source.slice(index, index + 4) === "<!--") {
                    var commentEnd = source.indexOf("-->", index + 4);
                    var comment = commentEnd === -1 ? source.slice(index) : source.slice(index, commentEnd + 3);
                    html += token("comment", comment);
                    index += comment.length;
                    continue;
                }

                var tagEnd = source.indexOf(">", index + 1);
                if (tagEnd === -1) {
                    html += escapeHtml(source.slice(index));
                    break;
                }

                var tag = source.slice(index, tagEnd + 1);
                var match = tag.match(/^(<\/?)([A-Za-z][A-Za-z0-9:-]*)([\s\S]*?)(\/?>)$/);
                if (!match) {
                    html += escapeHtml(tag);
                    index = tagEnd + 1;
                    continue;
                }

                var attributes = match[3].replace(
                    /([A-Za-z_:][A-Za-z0-9_:.-]*)(\s*=\s*)("[^"]*"|'[^']*'|[^\s"'>/]+)/g,
                    function (_attribute, name, equals, value) {
                        return token("attribute", name) + escapeHtml(equals) + token("string", value);
                    }
                );

                html += token("punctuation", match[1]) + token("tag", match[2]) + attributes + token("punctuation", match[4]);
                index = tagEnd + 1;
                continue;
            }

            html += escapeHtml(current);
            index++;
        }

        return html;
    }

    function highlightCss(source) {
        var html = "";
        var index = 0;
        var inDeclaration = false;

        while (index < source.length) {
            var current = source[index];
            var next = source[index + 1] || "";

            if (current === "/" && next === "*") {
                var commentEnd = readBlockComment(source, index);
                html += token("comment", source.slice(index, commentEnd));
                index = commentEnd;
                continue;
            }

            if (current === "\"" || current === "'") {
                var quotedEnd = readQuoted(source, index, current);
                html += token("string", source.slice(index, quotedEnd));
                index = quotedEnd;
                continue;
            }

            if (current === "{") {
                inDeclaration = true;
                html += token("punctuation", current);
                index++;
                continue;
            }

            if (current === "}") {
                inDeclaration = false;
                html += token("punctuation", current);
                index++;
                continue;
            }

            var number = source.slice(index).match(/^-?\d+(\.\d+)?(%|[A-Za-z]+)?/);
            if (number) {
                html += token("number", number[0]);
                index += number[0].length;
                continue;
            }

            var identifier = source.slice(index).match(/^(@?[A-Za-z_-][A-Za-z0-9_-]*)/);
            if (identifier) {
                var word = identifier[0];
                var after = source.slice(index + word.length);
                if (cssAtRules.has(word)) {
                    html += token("keyword", word);
                } else if (inDeclaration && after.match(/^\s*:/)) {
                    html += token("property", word);
                } else if (!inDeclaration) {
                    html += token("selector", word);
                } else {
                    html += escapeHtml(word);
                }
                index += word.length;
                continue;
            }

            if (/[{}():;,.#>+~*=|$^]/.test(current)) {
                html += token("punctuation", current);
                index++;
                continue;
            }

            html += escapeHtml(current);
            index++;
        }

        return html;
    }

    function highlightScriptLike(source) {
        return highlightCSharp(source);
    }

    function highlightBash(source) {
        return escapeHtml(source)
            .replace(/(^|\s)(#[^\n]*)/g, function (_, prefix, comment) {
                return prefix + token("comment", comment);
            })
            .replace(/\b(cd|cp|dotnet|echo|git|mkdir|npm|rm|yarn)\b/g, function (match) {
                return token("keyword", match);
            })
            .replace(/(--?[A-Za-z0-9-]+)/g, function (match) {
                return token("attribute", match);
            });
    }

    function highlightSql(source) {
        return escapeHtml(source).replace(
            /\b(SELECT|FROM|WHERE|JOIN|LEFT|RIGHT|INNER|OUTER|ON|GROUP|ORDER|BY|INSERT|UPDATE|DELETE|CREATE|ALTER|DROP|TABLE|INTO|VALUES|SET|AND|OR|NOT|NULL|IS|IN|AS|TOP|LIMIT)\b/gi,
            function (match) {
                return token("keyword", match);
            }
        ).replace(/'[^']*'/g, function (match) {
            return token("string", match);
        });
    }

    function highlightMarkdown(source) {
        return escapeHtml(source)
            .replace(/^(\s{0,3}#{1,6}\s.*)$/gm, function (match) {
                return token("keyword", match);
            })
            .replace(/(`[^`]+`)/g, function (match) {
                return token("string", match);
            });
    }

    function highlightSource(source, language) {
        switch (language) {
            case "csharp":
                return highlightCSharp(source);
            case "json":
                return highlightJson(source);
            case "markup":
            case "xml":
                return highlightMarkup(source);
            case "css":
                return highlightCss(source);
            case "javascript":
            case "typescript":
                return highlightScriptLike(source);
            case "bash":
                return highlightBash(source);
            case "sql":
                return highlightSql(source);
            case "markdown":
                return highlightMarkdown(source);
            default:
                return escapeHtml(source);
        }
    }

    function applyLineNumbers(code) {
        var lines = code.innerHTML.split(/\n/);
        code.innerHTML = lines.map(function (line, index) {
            return "<span class=\"dmb-code-block-line\"><span class=\"dmb-code-block-line-number\">" +
                (index + 1) +
                "</span><span class=\"dmb-code-block-line-code\">" +
                (line || " ") +
                "</span></span>";
        }).join("");
    }

    function enhanceBlock(block) {
        var code = block.querySelector(".dmb-code-block-code");
        if (!code || code.getAttribute("data-code-block-highlighted") === "true") {
            return;
        }

        var language = block.getAttribute("data-code-block-language") || "none";
        var rawCode = code.textContent || "";
        code.setAttribute("data-code-block-raw", rawCode);
        code.innerHTML = block.getAttribute("data-code-block-highlight") === "true"
            ? highlightSource(rawCode, language)
            : escapeHtml(rawCode);

        if (block.getAttribute("data-code-block-line-numbers") === "true") {
            applyLineNumbers(code);
        }

        code.setAttribute("data-code-block-highlighted", "true");
    }

    function getCodeText(block) {
        var code = block.querySelector(".dmb-code-block-code");
        return code ? code.getAttribute("data-code-block-raw") || code.textContent || "" : "";
    }

    function setCopyIcon(button, copied) {
        var icon = button.querySelector(".bi");
        if (!icon) {
            return;
        }

        icon.classList.toggle("bi-clipboard", !copied);
        icon.classList.toggle("bi-clipboard-check", copied);
    }

    function setCopiedState(button) {
        button.setAttribute("data-code-block-copy-state", "copied");
        setCopyIcon(button, true);
        window.setTimeout(function () {
            button.removeAttribute("data-code-block-copy-state");
            setCopyIcon(button, false);
        }, 1600);
    }

    function copyCode(button) {
        var block = button.closest("[data-code-block='true']");
        if (!block) {
            return;
        }

        var text = getCodeText(block);
        if (!text || !navigator.clipboard) {
            return;
        }

        navigator.clipboard.writeText(text).then(function () {
            setCopiedState(button);
        });
    }

    document.addEventListener("click", function (event) {
        var button = event.target.closest("[data-code-block-copy='true']");
        if (!button) {
            return;
        }

        copyCode(button);
    });

    function enhanceAll(root) {
        var scope = root || document;
        if (scope.matches && scope.matches("[data-code-block=\"true\"]")) {
            enhanceBlock(scope);
        }

        scope.querySelectorAll("[data-code-block=\"true\"]").forEach(enhanceBlock);
    }

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", function () {
            enhanceAll(document);
        });
    } else {
        enhanceAll(document);
    }

    if (window.MutationObserver) {
        new MutationObserver(function (mutations) {
            mutations.forEach(function (mutation) {
                mutation.addedNodes.forEach(function (node) {
                    if (node.nodeType === 1) {
                        enhanceAll(node);
                    }
                });
            });
        }).observe(document.documentElement, { childList: true, subtree: true });
    }

    window.DMBCodeBlock = window.DMBCodeBlock || {};
    window.DMBCodeBlock.enhance = enhanceAll;
})();
