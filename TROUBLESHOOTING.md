# DMBComponentBuilder Troubleshooting

## Purpose

Collect common issues and investigation paths for `DMBComponentBuilder`.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBComponentBuilder`
- Project folder: `DMBComponentBuilder`
- Project role: reusable visual component package for PageBuilder and BootstrapBuilder applications.
- Publication host: `labs_idemobi_com`

## Component output is missing or empty

Check:

- the component received the expected definition items,
- the builder did not intentionally render an empty state,
- required labels, titles, URLs, or child content were provided,
- repeated fluent calls did not replace previous state unexpectedly.

## Component styling does not appear

Check:

- consuming applications register `ComponentConfigureOptions`,
- embedded assets are exposed through the host application,
- generated class names match the CSS selectors,
- BootstrapBuilder and PageBuilder dependencies are configured.

## Code or copy block behavior is wrong

Check:

- code language and theme settings,
- copy target values,
- output encoding for snippets,
- generated button labels and accessibility text,
- whether examples accidentally include real secrets.

## Progress, timeline, or roadmap state is wrong

Check:

- item order,
- active, completed, pending, warning, and error states,
- duplicated item identifiers,
- state-specific classes and labels,
- empty and fallback state rendering.

## Social-share links are wrong

Check:

- platform enum selection,
- generated URL encoding,
- title, text, and target URL values,
- whether consuming pages provide absolute URLs when required.

## Documentation page issues

When pages in `labs_idemobi_com` are wrong or inconsistent:

- read `EXAMPLES_AND_TUTORIALS_RULES.md`,
- use `CodeBlockBuilder` or `Html.CodeBlock(...)` for code examples,
- use `ActionItem` with `ButtonRender` for action links,
- use `DRAWIO_DIAGRAM_RULES.md` for editable diagrams,
- keep DocumentationViewer links targeting `DMBComponentBuilder`.
