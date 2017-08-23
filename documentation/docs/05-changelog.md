## CHANGELOG

### v.1.1.0

*08/01/2017*

*Please note: our next release will end support for the Unity 5.x lifecycle. We look forward to taking advantage of Unity 2017 optimizations and features.*

##### Upgrade Instructions
- As always, please remove older versions before installing!
- `TypeFilter` now uses an array. You will need to update your filters!
- `MeshFactory` is obsolete. Please replace with `VectorTileFactory` or `StyleOptimizedVectorTileFactory`.
- `PoiVisualizer` is obsolete. Please use a standard `VectorLayerVisualizer` with a `PrefabModifier`.
- Triangle.NET has been replaced with `Earcut`. If you were using Triangle features, you will need to [import that library yourself](https://github.com/mapbox/triangle.net-uwp).
- If you were using a `RangeTileProvider`, you will need to update its parameters, which are now described in terms of North, East, South, and West (for readability).
- `ChamferModifier` has been replaced with `ChamferHeightModifier`. This new version looks and performs better.
- Ensure your `MapImageFactories` have the expected `Map Id`! We've updated the way Ids are serialized, and your old Ids may have been lost--sorry!

##### Memory/Performance
- Added support for style-optimized vector tiles! [Read more here.](https://www.mapbox.com/api-documentation/#retrieve-tiles)
    - You will need to use the new `StyleOptimizedVectorTileFactory`.
    - This can result in far less data transfer/data processing.
- Replaced Triangle.NET with `Earcut` which results if much faster geometry construction.
- Removed expensive string concatenation process in `FeatureBehaviour`.
- `MeshFactory` no longer waits for `Terrain` or `Raster` results before making its own web request.

##### New Features
- Added ability to cache successful tile requests to disk (via SQL database). If a tile is found in the database, it will not make a web request.
    - Note that tiles in this database only expire when the cache fills up!
- Want to create a low-poly landscape? Use the new `LowPolyTerrainFactory`!
- Maps can now be snapped to `y=0` (this prevents the need to reposition your camera at higher elevations).
- Added ability to choose pivot of objects generated with the `ModifierStack` (tile center, first vertex, or vertex average).
- Use the new `GameObjectModifier` `AddMonoBehavioursModifier` to "generically" add `Components` to your game objects.
- Added ability to snap to terrain/other objects with `SnapTerrainRaycastModifier`. This is more accurate (but slower) than `SnapTerrainModifier`.
- Added "example" `SpawnInsideModifier` which can be used to add procedural decoration inside a mesh (for example, `Landuse`).
- Added `ChamferHeightModifier` which, obviously, combines `Chamfer` and `Height` modifiers.
- Added `GlobeTileProvider` to request the entire world at once (be advised that this may result in MANY tile requests!).
- Project your tiles on a sphere! Use the new `GlobeTerrainFactory` to make a globe.
- Added ability to pick a custom style from the Mapbox Styles API.
    - Use the search button and enter your user name (for Mapbox Studio).
    - Note that this requires you to [create an API token](https://www.mapbox.com/studio/account/tokens/) that supports `styles:list`!

##### Bug Fixes
- WebGL builds now work as expected!
- Fixed Unity Cloud Build and iOS signing issues that were related to native iOS libraries.
- Map tiles are now parented correctly to the map root (on device). If you previously had trouble rotating/moving/scaling a map on device, fret no longer!
- Fixed some bad height calculations for buildings.
- `PolygonModifier` now correctly generates holes and better handles multiple `parts`.
- Tiles should now wrap at world boundaries correctly (rather than producing invalid tile requests).

##### Improvements
- Unity/C# warnings have been addressed.
- Added preprocess build step and a unit test that checks for duplicate Android libraries.
- Example `CameraMovement` script has been improved to allow for mouse wheel translational zoom and precise touch drag (for panning).
- Added option to remove sidewalls from height-extruded geometry (for roads, for example).
- Use `VertexDebugger` to debug vertices for procedurally generated meshes.
- Added timeout exception to `HttpRequest`.
- RangeTileProvider is now more readable.

##### New Examples
- See `Globe` example for one method of spherically projecting tiles.
- `MeshGenerationBasics` has new interactive elements to demonstrate some of the new features we've added.
- `StylingDemoMeshGeneration` uses `SpawnInsideModifier` to add "bushes" to `landuse:park`.
- Check `TerracedWorld` to see an example of how to use contour data to generate Godus-like worlds.

### v.1.0.0

*05/26/2017*

##### Memory/Performance

- Added support for runtime texture compression (DXT) in the `MapImageFactory`
- `MapVisualizer` now pools gameobjects/textures/data to avoid instantiation and destruction costs
- TerrainFactory now allocates less memory when manipulating geometry
- Elevation textures are no longer held in memory and height data parsing and access is much faster
- Added new `FlatTerrainFactory` that is optimized specifically for flat maps
- Tiles can now be cached in memory—configure the cache size in `MapboxAccess.cs` (default size is 500)
- Slippy maps now dispose tiles that determined to be "out of range" 
  - Tiles that are out of range before completion are properly cancelled
- Terrain generation in Unity 5.5+ should be much faster and allocate less memory

##### New Features


- Added new retina-resolution raster tiles
- Added mipmap, compression, and retina-resolution support to `MapImageFactory`
- The `PoiGeneration` example now includes clickable 3D world-space gameobjects—use these as reference for placing objects in Unity space according to a latitude/longitude
- `MapVisualizer` and `TileFactories` now invoke state change events—use these to know when a map or specific factory is completed (loaded)

  - See an example of implementing a loading screen in `Drive.unity`
- You can now specify GameObject `Layer` for tiles in the `TerrainFactory`
- Add colliders to your terrain by checking the `Add Collider` flag in the `TerrainFactory`
- Add colliders or specify GameObject `Layer` for buildings, roads, etc. with `ColliderModifier` and `LayerModifier`

##### Bug Fixes

- Building snapped to terrain are now rendered correctly (check `Flat Tops` in the `HeightModifier`)
- Web request exceptions are now properly forwarded to the `Response` (should fix `Unknown tile tag: 15`)
- Complex building geometry should now be rendered correctly (holes, floating parts, etc.)
- Materials assigned to a `TerrainFactory` are now properly applied at runtime
- Because of `UnityTile` pooling, you should no longer encounter `key already exists in dictionary` exceptions related to tile factories—this means you can change map attributes (location, zoom, terrain, etc.) at runtime without throwing exceptions

##### Improvements

- Map configuration values are no longer static, and an `OnInitialized` event is invoked when the `AbstractMap` reference values have been computed (prevents temporal coupling)
- Snapping to terrain has been simplified—just add a `SnapToTerrainModifier` to your `ModifierStack`
- `Slippy.cs` has been refactored to `CameraBoundsTileProvider.cs` and the backing abstraction enables you to write your own tile provider system (zoomable, path-based, region, etc.)
- `MapController.cs` has been refactored to `AbstractMap` —this is not yet abstract, but should provide an example of how to construct a map using a `MapVisualizer` and a `TileProvider`
- `UnityTile` has been refactored to support reuse and has the ability to cancel its backing web requests
- `DirectionsFactory` no longer relies on a `MapVisualizer` or `DirectionsHelper`, but can still use existing `MeshModifiers`

### v0.5.1

*05/01/2017*

- Terrain height works as intended again (fixed out of range exception)
- Fixed issue where visualizers for `MeshFactories` were not being serialized properly
- Fixed null reference exception when creating a new `MeshFactory`

### v0.5.0 

*04/26/2017*

- Added support for UWP 
    - Share your Hololens creations with us! 
- Fixed precision issue with tile conversions
    - Replaced `Geocoordinate` with `Vector2d`
- Mapbox API Token is now stored in MapboxAccess.txt
    - `MapboxConvenience` has been removed
- Added `LocationProviders` and example scene to build maps or place objects based on a latitude/longitude
- Mesh Generation:
    - General performance improvements (local tile geometry)
    - Custom editors for map factories
    - Added new `MergedModifierStack` which will reduce the number of transforms and draw calls in dense maps
    - Continuous UVs for building facades
    - `DirectionsFactory` now draws full geometry, not just waypoints
    - Fixed occasional vertex mismatch in `PolygonMeshModifier.cs` (which caused an index out of range exception)

### v0.4.0
- Updates mapbox-sdk-unity-core to v1.0.0-alpha13; features vector tile overzooming
  - Updates to attribution guidelines in README.MD
  - Added Conversions.cs and VectorExtensions.cs to enable simple conversions from geocoordinate to unity coordinate space

### v0.3.0
- Added new infrastructure for mesh generation
  - Added new demos for basic, styled, point of interest vector mesh generation
  - Added new demo for vector tiles + terrain with a slippy implementation (dynamic tile loading)
  - Added a new demo for Mapbox Directions & Traffic
  - Deprecated old slippy demo
  - Deprecated old directions component demo

### v0.2.0
- Added core sdk support for mapbox styles
  - vector tile decoding optimizations for speed and lazy decoding
  - Added attribution prefab
  - new Directions example
  - All examples scripts updated streamlined to use MapboxConvenience object

### v0.1.1
- removed orphaned references from link.xml, this was causing build errors
  - moved JSON utility to Mapbox namespace to avoid conflicts with pre-exisiting frameworks
