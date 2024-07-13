using Common;
using Configs.TextureRepository;
using UnityEngine;

namespace Global
{
	public class ConfigurableRoot : Singletone<ConfigurableRoot>
	{
		[SerializeField] private ImageRepositoryConfig _imageRepositoryConfig;
		public ImageRepositoryConfig ImageRepositoryConfig => _imageRepositoryConfig;
	}
}