# DMBComponentBuilder

Reusable Razor visual components for PageBuilder and BootstrapBuilder applications.

## Components

- `AvatarBuilder` renders reusable profile avatars with icons, images, initials,
  custom colors, sizes, contained badges, and a small JavaScript helper for live
  preview updates and image fallback.
- `ConversationBuilder` renders participant-based message threads for support tickets,
  forums, moderation discussions, and other multi-person conversations. Each
  `ConversationMessage` renders its participant avatar through `AvatarBuilder`
  and can optionally attach a contained badge with `AvatarBadgeText` and
  `AvatarBadgeVariant`. Profile palettes can be passed exactly with
  `AvatarBackgroundColor` and `AvatarForegroundColor`. Message-level badges can
  be added with `ConversationMessage.AddBadge(...)` or the `Badges` collection
  to render compact status, category, or visibility metadata in the message
  metadata row.
- `FestivalBuilder` renders a festival partial from a `GDFFestival` definition.
  `FestivalManager` centralizes the shared injector list and date-based selection
  for applications that provide their own festival definitions.
- `ChartJsBuilder` renders a Chart.js canvas from a `ChartJSCore.Models.Chart`
  definition and registers the Chart.js runtime.
  Initialization waits for the Chart.js runtime so pages remain stable when
  scripts are emitted after component markup.
  The labs host exposes live examples at `/ChartJs/Index`.
