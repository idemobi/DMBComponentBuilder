# DMBComponentBuilder Architecture Decisions

## Purpose

Record durable architecture decisions that AI assistants and maintainers must preserve unless a change request explicitly supersedes them.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBComponentBuilder`
- Project folder: `DMBComponentBuilder`
- Project role: reusable visual component package for PageBuilder and BootstrapBuilder applications.
- Main dependency: `DMBBootstrapBuilder`.
- Publication host: `labs_idemobi_com`

## Decisions

### Keep component builders fluent and deterministic

Builder methods should return the same builder instance for chaining and should update rendering state predictably.

### Keep component families isolated

Timeline, roadmap, code block, file tree, FAQ, social share, rating badge, step, and web-comic behavior should remain in their own component folders.

### Keep styling integrated with BootstrapBuilder

Components should use existing BootstrapBuilder and PageBuilder conventions for layout, classes, actions, and rendering instead of introducing an independent UI framework.

### Keep component state explicit

State enums and definition classes must describe user-facing component states clearly and must keep generated output stable.

### Treat rendered text and code carefully

Code snippets, copyable text, social-share values, file paths, labels, and user-provided strings must be documented and encoded according to their rendered context.

### Keep examples outside the package

Example pages, tutorials, diagrams, and explanatory pages are published through `labs_idemobi_com` when requested.

The package should not embed documentation website pages directly.
