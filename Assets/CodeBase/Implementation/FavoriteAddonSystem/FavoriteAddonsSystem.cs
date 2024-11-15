using System;
using System.Collections.Generic;
using CodeBase.Core.Systems.DataPersistenceSystem;

namespace CodeBase.Implementation.FavoriteAddonSystem
{
	public class FavoriteAddonsSystem : IDataPersistence
	{
		public event Action<int> OnFavoriteCardRemoved;
		
		public List<int> FavoriteAddons { get; private set; } = new List<int>();

		public void AddToFavorites(int addonID)
		{
			if (!FavoriteAddons.Contains(addonID)) FavoriteAddons.Add(addonID);
		}

		public void RemoveFromFavorites(int addonID)
		{
			if (!FavoriteAddons.Contains(addonID)) return;
			FavoriteAddons.Remove(addonID);
			OnFavoriteCardRemoved?.Invoke(addonID);
		}

		public bool CheckIfAddonFavorite(int addonID) => FavoriteAddons.Contains(addonID);

		public void LoadData(GameData gameData)
		{
			FavoriteAddons = gameData.favoriteAddonsID;
		}

		public void SaveData(ref GameData gameData)
		{
			gameData.favoriteAddonsID = FavoriteAddons;
		}
	}
}
