# DMBComponentBuilder

Reusable Razor visual components for PageBuilder and BootstrapBuilder applications.

## Components

- `ConversationBuilder` renders participant-based message threads for support tickets,
  forums, moderation discussions, and other multi-person conversations.
- `FestivalBuilder` renders a festival partial from a `GDFFestival` definition.
  `FestivalManager` centralizes the shared injector list and date-based selection
  for applications that provide their own festival definitions.
- `ChartJsBuilder` renders a Chart.js canvas from a `ChartJSCore.Models.Chart`
  definition and registers the Chart.js runtime.
  Initialization waits for the Chart.js runtime so pages remain stable when
  scripts are emitted after component markup.
  The labs host exposes live examples at `/ChartJs/Index`.
