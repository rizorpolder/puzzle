using Common;
using Configs.TextureRepository;
using UnityEngine;

namespace Global
{
	public class ConfigurableRoot : Singletone
	{
		[SerializeField] private ImageRepositoryConfig _imageRepositoryConfig;
		public ImageRepositoryConfig ImageRepositoryConfig => _imageRepositoryConfig;
	}
}