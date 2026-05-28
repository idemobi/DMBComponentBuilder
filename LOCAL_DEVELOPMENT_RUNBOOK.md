# DMBComponentBuilder Local Development Runbook

## Purpose

Provide a lightweight workflow for local work in `DMBComponentBuilder`.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBComponentBuilder`
- Project folder: `DMBComponentBuilder`
- Project role: reusable visual component package for PageBuilder and BootstrapBuilder applications.
- Publication host: `labs_idemobi_com`

## Orientation

Start by reading:

- [README.md](README.md)
- [PROJECT_MAP.md](PROJECT_MAP.md)
- [DOCUMENTATION_RULES.md](DOCUMENTATION_RULES.md)
- [DELIVERY_CHECKLIST.md](DELIVERY_CHECKLIST.md)

For example or tutorial page work, also read:

- [EXAMPLES_AND_TUTORIALS_RULES.md](EXAMPLES_AND_TUTORIALS_RULES.md)
- [DRAWIO_DIAGRAM_RULES.md](DRAWIO_DIAGRAM_RULES.md)

## Work loop

1. Identify the affected component family:
   - block titles,
   - checkout progress,
   - code blocks,
   - copy blocks,
   - FAQ blocks,
   - festival blocks,
   - file trees,
   - mind maps,
   - rating badges,
   - roadmaps,
   - separators and shields,
   - social share blocks,
   - steps,
   - timelines,
   - todo blocks,
   - web-comic viewers,
   - configuration,
   - documentation pages.
2. Read the relevant code and local rules before editing.
3. Keep edits local to the smallest useful area.
4. Update XML documentation for touched public APIs.
5. Update README or guidance files when behavior changes.
6. Run only checks that the user explicitly permits.

## Build and test policy

Do not run these commands unless explicitly requested:

```text
dotnet build
dotnet test
dotnet restore
dotnet format
```

## Safe inspection commands

Useful read-only commands:

```text
rg "CodeBlockBuilder" DMBComponentBuilder
rg "TimelineBuilder" DMBComponentBuilder
find DMBComponentBuilder -maxdepth 3 -type f | sort
git diff -- DMBComponentBuilder
```

Prefer `rg` for searches.
