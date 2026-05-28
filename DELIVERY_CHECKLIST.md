# DMBComponentBuilder Delivery Checklist

## Purpose

Use this checklist before finishing changes in `DMBComponentBuilder`.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBComponentBuilder`
- Project folder: `DMBComponentBuilder`
- Project role: reusable visual component package for PageBuilder and BootstrapBuilder applications.
- Publication host: `labs_idemobi_com`

## Code checklist

- Public API compatibility was preserved, or the breaking change was explicitly requested.
- New or changed public members have useful English XML documentation.
- Component builder names, extension method names, and definition types remain discoverable and consistent.
- Generated markup, CSS classes, data attributes, and embedded asset paths remain deterministic.
- Empty, error, and realistic data states were considered for changed visual components.
- Accessibility labels, headings, copy buttons, share links, and keyboard behavior were reviewed when relevant.
- Raw HTML, code snippets, URLs, and user-provided text were reviewed for encoding and rendering behavior.
- No unrelated files were reformatted or refactored.

## Documentation checklist

- README was updated when project behavior or usage changed.
- `DOCUMENTATION_RULES.md` was followed for XML docs and reference documentation.
- `EXAMPLES_AND_TUTORIALS_RULES.md` was used only for example, demo, information, instruction, concept, or tutorial pages.
- `DRAWIO_DIAGRAM_RULES.md` was followed when diagrams were added or updated.
- Documentation names `DMBComponentBuilder` concepts, not copied source-project concepts.
- Documentation is written in English unless the task explicitly requested another language for website content.

## Verification checklist

- Do not run `dotnet build`, `dotnet test`, `dotnet restore`, or `dotnet format` unless explicitly requested.
- If no build or tests were run, say so in the final response.
- If only text checks were run, name those checks precisely.
- Mention any remaining risks or manual validation needs.

## Final response checklist

- Summarize changed files.
- Mention that build/test were not run unless explicitly requested and actually executed.
- List follow-up items only when they are useful and specific.
