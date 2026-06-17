# DMBComponentBuilder Project Map

## Purpose

Map the structure of `DMBComponentBuilder` so AI assistants can find the right files quickly.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBComponentBuilder`
- Project folder: `DMBComponentBuilder`
- Project role: reusable visual component package for PageBuilder and BootstrapBuilder applications.
- Publication host: `labs_idemobi_com`

## Root files

- `AGENTS.md`: local AI instructions.
- `AI_CONTEXT.md`: project context for AI assistants.
- `DOCUMENTATION_RULES.md`: XML and reference documentation rules.
- `EXAMPLES_AND_TUTORIALS_RULES.md`: website page, example, and tutorial rules.
- `DRAWIO_DIAGRAM_RULES.md`: editable Draw.io diagram rules.
- `DELIVERY_CHECKLIST.md`: pre-delivery checklist.
- `ARCHITECTURE_DECISIONS.md`: durable architecture decisions.
- `LOCALIZATION_NOMENCLATURE.md`: localization key rules.
- `LOCAL_DEVELOPMENT_RUNBOOK.md`: local workflow guide.
- `TROUBLESHOOTING.md`: common issue guide.
- `GLOSSARY.md`: common term definitions.

## Source

- `Source/DMBComponentBuilder.csproj`: project file and package metadata.
- `Source/README.md`: package overview and documentation entry point.
- `Source/LICENSE.md`: package license file.
- `Source/DMBComponentBuilder.png`: package icon.
- `Source/DMBComponentBuilder.snk`: strong-name key file.
- `Source/Configuration`: package configuration classes.
- `Source/Components`: reusable visual component builders and models.
- `Source/wwwroot`: static web assets embedded by the package.
- `Labs/DMBComponentBuilderLabs.csproj`: Razor labs project for host-displayed documentation and examples.
- `UnitTests/DMBComponentBuilderUnitTest.csproj`: module unit test project.

## Source/Components/BlockTitle

- `BlockTitleBuilder.cs`: block title visual builder.
- `BlockTitleExtensions.cs`: helper entry points.
- `BlockTitleSubtitle.cs`: subtitle data model.
- `SubtitleSize.cs`: subtitle sizing enum.

## Source/Components/CheckoutProgress

- `CheckoutProgressBuilder.cs`: checkout progress visual builder.
- `CheckoutProgressDefinition.cs`: progress item data model.
- `CheckoutProgressExtensions.cs`: helper entry points.
- `CheckoutProgressState.cs`: progress state enum.

## Source/Components/CodeBlock

- `CodeBlockBuilder.cs`: code block visual builder.
- `CodeBlockComposer.cs`: code block composition helper.
- `CodeBlockExtensions.cs`: helper entry points.
- `CodeBlockTheme.cs`: code block theme enum.
- `CodeLanguage.cs`: supported code language enum.
- `CodeLanguageExtensions.cs`: language helper methods.

## Source/Components/CopyBlock

- `CopyBlockBuilder.cs`: copyable content builder.
- `CopyBlockExtensions.cs`: helper entry points.

## Source/Components/Faq

- `FaqBuilder.cs`: FAQ component builder.
- `FaqExtensions.cs`: helper entry points.
- `FaqItem.cs`: FAQ item data model.

## Source/Components/FileTree

- `FileTreeBuilder.cs`: file tree component builder.
- `FileTreeExtensions.cs`: helper entry points.
- `FileTreeNode.cs`: file tree node data model.

## Source/Components/MindMap

- `GDFMindMapBuilder.cs`: mind map component builder.
- `GDFMindMapExtensions.cs`: helper entry points.
- `GDFMindMapNode.cs`: mind map node data model.
- `GDFMindMapBranchSide.cs`: branch side enum.
- `GDFMindMapLineMode.cs`: line rendering enum.

## Source/Components/RatingBadge

- `RatingBadgeBuilder.cs`: rating badge component builder.
- `RatingBadgeComposer.cs`: rating badge composition helper.
- `RatingBadgeEffectComposer.cs`: rating badge effect composition helper.
- `RatingBadgeExtensions.cs`: helper entry points.
- `RatingBadgeSize.cs`: rating badge size enum.
- `ICanUseRatingBadgeEffects.cs`: fluent effect capability interface.
- `ICanUseRatingBadgeStyle.cs`: fluent style capability interface.

## Source/Components/Roadmap

- `RoadmapBuilder.cs`: roadmap container builder.
- `RoadmapBlockBuilder.cs`: roadmap block builder.
- `RoadmapDefinition.cs`: roadmap item data model.
- `RoadmapEffectComposer.cs`: roadmap effect composition helper.
- `RoadmapEffectExtensions.cs`: roadmap effect extensions.
- `RoadmapExtensions.cs`: helper entry points.
- `RoadmapSlideEffect.cs`: roadmap slide effect enum.
- `RoadmapState.cs`: roadmap state enum.

## Source/Components/SocialShare

- `SocialShareBuilder.cs`: social share component builder.
- `SocialShareDefinition.cs`: share item data model.
- `SocialShareExtensions.cs`: helper entry points.
- `SocialShareKind.cs`: share kind enum.
- `SocialSharePlatform.cs`: supported platform enum.
- `SocialShareStyle.cs`: share style enum.

## Source/Components/Step

- `StepAreaBuilder.cs`: step area builder.
- `StepAreaBuilder.Generic.cs`: generic step area builder.
- `StepBlockBuilder.cs`: step block builder.
- `StepBlockState.cs`: step state enum.
- `StepDefinition.cs`: step item data model.
- `StepExtensions.cs`: helper entry points.
- `StepRuleDefinition.cs`: step rule data model.

## Source/Components/Timeline

- `TimelineBuilder.cs`: timeline container builder.
- `TimelineBlockBuilder.cs`: timeline block builder.
- `TimelineDefinition.cs`: timeline item data model.
- `TimelineEffectComposer.cs`: timeline effect composition helper.
- `TimelineExtensions.cs`: helper entry points.
- `TimelineSlideEffect.cs`: timeline slide effect enum.
- `ICanUseTimelineEffects.cs`: fluent effect capability interface.

## Source/Components/Festival

- `FestivalBuilder.cs`: festival visual component builder.
- `FestivalExtensions.cs`: Razor helper entry points for festival rendering.
- `FestivalManager.cs`: shared festival injector registry and date selection helper.
- `GDFFestival.cs`: festival data model.
- `IFestivalInjector.cs`: contract for packages that provide festival definitions.

## Other component folders

- `Source/Components/Separator`: separator component builders and helpers.
- `Source/Components/Shield`: shield component builders and helpers.
- `Source/Components/TodoBlock`: todo block builders, helpers, and item model.
- `Source/Components/WebComic`: web-comic viewer builder, helpers, display modes, and social platform model.

## Configuration

- `ComponentBuilderConfiguration.cs`: default component builder configuration.
- `ComponentConfigureOptions.cs`: static file options configuration for embedded component assets.

## Related projects

- `DMBBootstrapBuilder`: Bootstrap-oriented visual builder package.
- `DMBPageBuilder`: low-level page and HTML builder package.
- `DMBFormBuilder`: form builder package used in examples and preview pages.
- `labs_idemobi_com`: publication host for examples, tutorials, information pages, and diagrams.
