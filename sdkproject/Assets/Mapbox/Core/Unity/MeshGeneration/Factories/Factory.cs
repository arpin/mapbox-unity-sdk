namespace Mapbox.Unity.MeshGeneration.Factories
{
    using UnityEngine;
    using Mapbox.Unity.MeshGeneration.Data;
    using Mapbox.Platform;

    public class Factory : ScriptableObject
    {
        protected IFileSource FileSource;

        public virtual void Initialize(MonoBehaviour mb, IFileSource fileSource)
        {
            FileSource = fileSource;
        }

        public virtual void Register(UnityTile tile)
        {

        }
    }
}