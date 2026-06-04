(function () {
    function getStep(stepOrId) {
        if (!stepOrId) {
            return null;
        }

        if (typeof stepOrId === "string") {
            return document.getElementById(stepOrId);
        }

        return stepOrId;
    }

    function setState(stepOrId, state) {
        var step = getStep(stepOrId);

        if (!step || !state) {
            return;
        }

        step.classList.remove("step-state-current");
        step.classList.remove("step-state-future");
        step.classList.remove("step-state-done");
        step.classList.remove("step-state-disabled");
        step.classList.add("step-state-" + state);

        if (step.tagName === "FIELDSET") {
            if (state === "disabled") {
                step.disabled = true;
                step.setAttribute("disabled", "disabled");
            } else {
                step.disabled = false;
                step.removeAttribute("disabled");
            }
        }

        refreshParentForm(step);
    }

    function refreshParentForm(step) {
        var form = step && step.closest ? step.closest("form") : null;

        if (!form) {
            return;
        }

        if (window.DMBFormBuilder && typeof window.DMBFormBuilder.refreshForm === "function") {
            window.DMBFormBuilder.refreshForm(form);
            return;
        }

        form.dispatchEvent(new CustomEvent("dmb-formbuilder-refresh", { bubbles: true }));
    }

    function activate(stepOrId) {
        setState(stepOrId, "current");
    }

    function disable(stepOrId) {
        setState(stepOrId, "disabled");
    }

    function done(stepOrId) {
        setState(stepOrId, "done");
    }

    function future(stepOrId) {
        setState(stepOrId, "future");
    }

    function initRules() {
        var scripts = document.querySelectorAll("script[type='application/json'][data-step-rules]");

        for (var index = 0; index < scripts.length; index++) {
            bindRules(scripts[index]);
        }
    }

    function bindRules(script) {
        var rules = parseRules(script);

        if (!rules.length) {
            return;
        }

        function refresh() {
            for (var index = 0; index < rules.length; index++) {
                applyRule(rules[index]);
            }
        }

        for (var index = 0; index < rules.length; index++) {
            var fieldIds = getRuleFieldIds(rules[index]);

            for (var fieldIndex = 0; fieldIndex < fieldIds.length; fieldIndex++) {
                var field = document.getElementById(fieldIds[fieldIndex]);

                if (!field) {
                    continue;
                }

                field.addEventListener("change", refresh);
                field.addEventListener("input", refresh);
            }
        }

        refresh();
    }

    function parseRules(script) {
        try {
            var rules = JSON.parse(script.textContent || "[]");
            return Array.isArray(rules) ? rules : [];
        } catch (error) {
            return [];
        }
    }

    function applyRule(rule) {
        if (!rule || !rule.targetStepId || !rule.condition) {
            return;
        }

        var activeState = rule.activeState || "current";
        var inactiveState = rule.inactiveState || "disabled";
        var isActive = evaluateRule(rule);

        setState(rule.targetStepId, isActive ? activeState : inactiveState);
    }

    function evaluateRule(rule) {
        if (rule.condition === "all-values") {
            return evaluateAllValues(rule);
        }

        var field = document.getElementById(rule.fieldId);

        if (!field || isDisabledByFieldset(field)) {
            return false;
        }

        if (rule.condition === "checked") {
            return field.checked === true && isFieldValid(field);
        }

        if (rule.condition === "value") {
            return hasFieldValue(field) && isFieldValid(field);
        }

        return false;
    }

    function evaluateAllValues(rule) {
        var fieldIds = getRuleFieldIds(rule);

        if (!fieldIds.length) {
            return false;
        }

        for (var index = 0; index < fieldIds.length; index++) {
            var field = document.getElementById(fieldIds[index]);

            if (!field || isDisabledByFieldset(field) || !hasFieldValue(field) || !isFieldValid(field)) {
                return false;
            }
        }

        return true;
    }

    function hasFieldValue(field) {
        if (field.type === "checkbox" || field.type === "radio") {
            return field.checked === true;
        }

        return (field.value || "").trim() !== "";
    }

    function isFieldValid(field) {
        if (typeof field.checkValidity === "function" && !field.checkValidity()) {
            return false;
        }

        var validator = getJQueryValidator(field);
        if (validator && typeof validator.element === "function") {
            return validator.element(field) !== false;
        }

        return true;
    }

    function getJQueryValidator(field) {
        if (!window.jQuery || !field.form) {
            return null;
        }

        var form = window.jQuery(field.form);
        return form.data("validator") || null;
    }

    function getRuleFieldIds(rule) {
        if (Array.isArray(rule.fieldIds) && rule.fieldIds.length) {
            return rule.fieldIds;
        }

        return rule.fieldId ? [rule.fieldId] : [];
    }

    function isDisabledByFieldset(field) {
        var fieldset = field.closest ? field.closest("fieldset") : null;
        return fieldset ? fieldset.disabled === true : false;
    }

    function initSwitches() {
        var switches = document.querySelectorAll("[data-step-switch]");

        for (var index = 0; index < switches.length; index++) {
            bindSwitch(switches[index]);
        }
    }

    function bindSwitch(toggle) {
        function refresh() {
            var activeState = toggle.getAttribute("data-step-active-state") || "current";
            var inactiveState = toggle.getAttribute("data-step-inactive-state") || "disabled";
            setState(toggle.getAttribute("data-step-switch"), toggle.checked ? activeState : inactiveState);
        }

        toggle.addEventListener("change", refresh);
        refresh();
    }

    window.DMBComponentBuilder = window.DMBComponentBuilder || {};
    window.DMBComponentBuilder.Step = {
        setState: setState,
        activate: activate,
        disable: disable,
        done: done,
        future: future,
        initRules: initRules,
        initSwitches: initSwitches
    };

    function init() {
        initSwitches();
        initRules();
    }

    if (document.readyState === "loading") {
        document.addEventListener("DOMContentLoaded", init);
    } else {
        init();
    }
})();
