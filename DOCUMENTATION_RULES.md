# DMBComponentBuilder Documentation Rules

## Language

- Documentation must be written in English.
- XML documentation comments must be written in English.

## Target audience

- Primary: developers maintaining or integrating `DMBComponentBuilder`.
- Secondary: developers building reusable visual components with the PageBuilder ecosystem.
- Tertiary: AI assistants consuming structured project rules and technical context.

Documentation must be useful without private chat context. A reader should understand what each component renders, which Bootstrap structures and embedded assets it uses, how fluent APIs configure component state, and what constraints apply before reading the implementation.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBComponentBuilder`
- Primary API families: visual component builders, component definitions, component states, component composers, Razor/helper extensions, component configuration, and embedded static assets.
- Important types to reference when relevant: `BlockTitleBuilder`, `CheckoutProgressBuilder`, `CodeBlockBuilder`, `CodeBlockComposer`, `CopyBlockBuilder`, `FaqBuilder`, `FileTreeBuilder`, `GDFMindMapBuilder`, `RatingBadgeBuilder`, `RoadmapBuilder`, `StepAreaBuilder`, `TimelineBuilder`, `TodoBlockBuilder`, `WebComicViewerBuilder`, `ComponentBuilderConfiguration`, and `ComponentConfigureOptions`.
- Publication host: `labs_idemobi_com`
- Documentation generation strategy: DocumentationBuilder-first; AI prepares content, the developer executes generation.

## Strict C# XML documentation policy

- Always write XML HeaderDoc for public classes, public interfaces, public structs, public records, public methods, public constructors, public properties, public fields, public constants, public events, public delegates, public enums, public enum values, and public extension methods.
- Also write XML HeaderDoc for protected members when they are part of a builder inheritance contract or are expected to be overridden by derived builders.
- Internal and private members do not require XML HeaderDoc unless they explain complex rendering, state mapping, accessibility, localization, or security behavior that would otherwise be difficult to maintain.
- XML documentation must use valid C# XML syntax.
- Prefer `<summary>`, `<param>`, `<typeparam>`, `<returns>`, `<value>`, `<remarks>`, `<exception>`, `<see cref="..."/>`, and `<seealso cref="..."/>`.
- Use `<inheritdoc/>` only when the inherited documentation is accurate for the current member.

## XML documentation quality standard

XML documentation must explain the public contract, not repeat the member name.

For component builders, document:

- the rendered visual component,
- the relationship with related definition, state, composer, and extension types,
- whether the type is used directly, through Razor helpers, or through fluent extension methods,
- accessibility and empty-state expectations when relevant.

For methods and constructors, document:

- what the member changes in generated component output, classes, attributes, labels, or child content,
- every parameter and expected format when relevant,
- the returned fluent builder instance when the method supports chaining,
- side effects such as adding items, replacing state, changing visual style, toggling an effect, adding copy/share behavior, or changing rendered attributes,
- validation rules and exceptions,
- whether `null`, empty strings, duplicate entries, repeated calls, or invalid values have special behavior.

For properties, fields, and constants, document:

- the meaning of the value,
- the default value when meaningful,
- whether consumers may set it directly,
- how it affects rendering, styling, state, localization, accessibility, or static assets.

For enums and enum values, document:

- where the enum is used,
- how each value maps to generated classes, rendered states, visual variants, or fallback behavior.

For extension methods, document:

- the receiver type,
- the builder returned,
- the intended Razor usage pattern,
- how the extension composes with existing BootstrapBuilder or PageBuilder output.

## Project API documentation requirements

- Component builders must identify the rendered component and the expected data shape.
- Definition types must document required and optional values.
- State enums must document how each value affects the rendered UI.
- Composer types must document class composition and effect composition behavior.
- Code and copy components must document encoding, copy behavior, and language/theme behavior.
- Social-share components must document supported platforms, generated URLs, and user-provided values.
- Navigation or progress components must document order, active/completed/error states, and accessibility labels.
- Static asset configuration must document embedded asset exposure and consuming application requirements.

## Examples in XML documentation

Use `<example>` when it materially improves understanding of Razor helper entry points, non-obvious fluent chains, component definitions, realistic state setup, or generated component structure.

Examples must be short, realistic, and compile-oriented. Prefer Razor examples for Razor helpers and C# examples for lower-level builder APIs.

## Markdown documentation policy

- Follow PageBuilder markdown conventions in `../MARKDOWN_GUIDELINES.md`.
- Keep this structure where applicable:
  1. Context
  2. Explanation
  3. Example
  4. Notes / constraints

## Draw.io diagrams for conceptual documentation

Information pages, instruction pages, concept pages, architecture pages, component lifecycle pages, and tutorial pages may use Draw.io diagrams when they clarify a real model or flow.

Draw.io diagrams must follow `DRAWIO_DIAGRAM_RULES.md`.

Do not use Draw.io diagrams in XML documentation comments. XML documentation may reference concepts that are diagrammed on pages, but the diagram artifact belongs to the website documentation layer.

## DocumentationBuilder-first rule

Documentation in this module must be authored with a DocumentationBuilder-first objective.

- Write docs so they can be extracted and rendered without manual rewrite.
- Keep headings deterministic and stable.
- Keep examples self-contained and realistically useful.
- Avoid implicit references to chat history or hidden context.
- Prefer stable type and member names that DocumentationBuilder can cross-reference.
- Use `<see cref="..."/>` and `<seealso cref="..."/>` for related PageBuilder types whenever it improves navigation.

## Separation from examples and tutorials

`EXAMPLES_AND_TUTORIALS_RULES.md` is not a general documentation rule source.

- Use this file for API documentation, XML HeaderDoc, README updates, reference pages, and DocumentationBuilder-ready documentation.
- Use `../MARKDOWN_GUIDELINES.md` for general Markdown formatting rules.
- Use `EXAMPLES_AND_TUTORIALS_RULES.md` only when the task explicitly creates or updates example pages, demo pages, information pages, instruction pages, concept pages, tutorials, or tutorial-like walkthroughs.

## Minimum update policy

If public component rendering behavior, state behavior, extension behavior, static asset behavior, or accessibility behavior changes, update in the same change set:

- local `README.md`,
- relevant XML docs,
- impacted guidance/examples when the task includes pages.

## Review checklist for documentation changes

- The documentation names real `DMBComponentBuilder` concepts, not copied source-project concepts.
- All public and protected-contract API members touched by the change have valid XML documentation.
- Summaries are specific enough to help IntelliSense users choose the right API.
- Parameters, return values, generic parameters, exceptions, and side effects are documented where applicable.
- Examples reflect current code behavior and realistic Razor component usage.
- Draw.io diagrams, when added, follow `DRAWIO_DIAGRAM_RULES.md`.
- DocumentationBuilder can extract the content without needing hidden context or manual rewrite.
