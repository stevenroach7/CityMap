namespace Mapbox.Unity.MeshGeneration.Factories
{
	using Mapbox.Platform;
	using Mapbox.Unity.MeshGeneration.Data;
	using System;
	using UnityEngine;
	using Mapbox.Unity.Map;
	using System.Collections.Generic;

	public abstract class AbstractTileFactory : ScriptableObject
    {
		protected IFileSource _fileSource;

		protected Dictionary<string, float> _uiData;
        
		public ModuleState State { get; private set; }

        private int _progress;
        protected int Progress
        {
            get
            {
                return _progress;
            }
            set
            {
                if (_progress == 0 && value > 0)
                {
                    State = ModuleState.Working;
                    OnFactoryStateChanged(this);
                }
                if (_progress > 0 && value == 0)
                {
                    State = ModuleState.Finished;
                    OnFactoryStateChanged(this);
                }
                _progress = value;                
            }
        }

		public event Action<AbstractTileFactory> OnFactoryStateChanged = delegate { };

		public void Initialize(IFileSource fileSource, Dictionary<string, float> uiData = null) // TODO: Add uiData dictionary SR.
        {
			_progress = 0;
			_fileSource = fileSource;
			_uiData = uiData;

            State = ModuleState.Initialized;
            OnInitialized();
        }

        public void Register(UnityTile tile)
        {
            OnRegistered(tile);
        }

        public void Unregister(UnityTile tile)
        {
            OnUnregistered(tile);
        }

        internal abstract void OnInitialized();

        internal abstract void OnRegistered(UnityTile tile);

        internal abstract void OnUnregistered(UnityTile tile);
    }
}