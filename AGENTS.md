# AI Rules - DMBComponentBuilder

## Scope

- Applies to the `DMBComponentBuilder` folder and descendants.
- This project is autonomous: required rules are defined in local documentation files.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBComponentBuilder`
- Project folder: `DMBComponentBuilder`
- Project role: reusable visual component package for PageBuilder and BootstrapBuilder applications.
- Primary consumers: Razor views and PageBuilder ecosystem packages that need timeline, roadmap, code, sharing, FAQ, file tree, rating, and editorial components.
- Main dependency: `DMBBootstrapBuilder`.
- Publication host: `labs_idemobi_com`
- Documentation generation strategy: DocumentationBuilder-first; AI prepares content, the developer executes generation.

## Module intent

- Provide reusable, fluent visual components that sit above Bootstrap primitives.
- Keep component markup, CSS classes, embedded assets, and fluent APIs deterministic for consuming Razor pages.
- Avoid moving low-level HTML primitives, general Bootstrap layout primitives, form-field behavior, or ASP.NET middleware responsibilities into this package.

## Key constraints

- Keep public APIs backward compatible unless a change request explicitly allows breakage.
- Prefer additive component options over structural rewrites.
- Every new visual component must include a demo or preview page in the publication host when the task includes component delivery.
- Cover normal state, empty state, error state, and one realistic data example for new component demos.
- Treat raw HTML, code snippets, social-share URLs, icon names, and user-provided text as output-encoding-sensitive areas.
- Do not run `dotnet build`, `dotnet test`, `dotnet restore`, or `dotnet format` unless explicitly requested.

## Documentation objective

- Documentation must be authored so it can be extracted and rendered by DocumentationBuilder.
- Publication target is `labs_idemobi_com`.
- Documentation output must serve both developers and AI assistants.
- AI prepares documentation content and structure; the developer runs DocumentationBuilder.
- XML documentation comments must be written in English.
- Public classes, public methods, public constructors, public properties, public fields, public constants, public enums, public enum values, public records, and extension methods must have useful XML documentation.

## Local rule sources

- Use [AI_CONTEXT.md](AI_CONTEXT.md) for the project summary and safe-change strategy.
- Use [DOCUMENTATION_RULES.md](DOCUMENTATION_RULES.md) for XML HeaderDoc, README/reference documentation, and DocumentationBuilder-ready documentation.
- Use [EXAMPLES_AND_TUTORIALS_RULES.md](EXAMPLES_AND_TUTORIALS_RULES.md) only when creating or updating example, demo, information, instruction, concept, or tutorial pages.
- Use [DRAWIO_DIAGRAM_RULES.md](DRAWIO_DIAGRAM_RULES.md) when adding editable Draw.io diagrams to information, instruction, concept, architecture, component lifecycle, or tutorial pages.
- Use `CodeBlockBuilder` or the local `Html.CodeBlock(...)` helper for code examples when available.
- Use `ActionItem` with `ButtonRender` for page action links when the target publication project exposes those helpers.
- Store editable Draw.io diagrams as enriched `.drawio.svg` files under `labs_idemobi_com/wwwroot/drawio/{Area}/`.

## Localization

- Follow local [LOCALIZATION_NOMENCLATURE.md](LOCALIZATION_NOMENCLATURE.md).
- Do not assume external localization rules unless duplicated here.

## Before delivery

- Update local docs when behavior changes.
- State untested areas explicitly.
- Do not claim build/test or DocumentationBuilder execution when they were not run.
