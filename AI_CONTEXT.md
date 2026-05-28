# DMBComponentBuilder AI Context

## Purpose

This file gives AI assistants the minimum project context required to work safely in `DMBComponentBuilder`.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBComponentBuilder`
- Project folder: `DMBComponentBuilder`
- Project role: reusable visual component package for PageBuilder and BootstrapBuilder applications.
- Main dependency: `DMBBootstrapBuilder`.
- Publication host: `labs_idemobi_com`
- Primary documentation audience: developers building reusable Razor page components.

## What this project is

`DMBComponentBuilder` is a reusable visual component package.

It provides:

- editorial blocks such as block titles, FAQs, separators, shields, and todo blocks,
- documentation and developer-facing components such as code blocks, copy blocks, and file trees,
- navigation and progression components such as steps, checkout progress, roadmaps, and timelines,
- social and presentation components such as share buttons, rating badges, mind maps, festivals, and web-comic viewers,
- component configuration and embedded static assets.

## What this project is not

This project is not:

- a low-level HTML builder package,
- a general Bootstrap layout package,
- a form builder package,
- an ASP.NET middleware package,
- a documentation website.

## Main concepts

- Builder classes render one reusable visual component and expose a fluent API for component state.
- Extension classes expose convenient entry points from existing PageBuilder or BootstrapBuilder objects.
- Definition classes represent repeatable component data such as steps, roadmap items, timeline entries, file tree nodes, and social share options.
- Composer classes centralize reusable class or effect composition for components such as code blocks, rating badges, roadmaps, and timelines.
- Configuration classes register embedded component assets for consuming MVC/Razor applications.

## Change strategy

- Keep changes localized to the relevant component family.
- Preserve public API names and behavior unless the request explicitly asks for a breaking change.
- Keep generated markup, class names, and embedded asset paths deterministic.
- Document public API behavior in XML comments when the code is touched.
- Update README and local rule files when project behavior or documentation strategy changes.

## Documentation strategy

- Use `DOCUMENTATION_RULES.md` for XML docs, README/reference docs, and DocumentationBuilder-ready documentation.
- Use `EXAMPLES_AND_TUTORIALS_RULES.md` only for pages, examples, tutorials, and walkthroughs.
- Use `DRAWIO_DIAGRAM_RULES.md` when diagrams clarify component composition, rendering flow, asset flow, or component state.
- Keep all generated documentation in English unless the user explicitly requests another language for user-facing website content.
