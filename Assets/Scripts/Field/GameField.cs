using Data;
using Global;

namespace Field
{
	public class GameField
	{
		public void CreateNewGameField(FieldData data)
		{
			var temp = ConfigurableRoot.Instance as ConfigurableRoot;

			// //Create/enable/disable elements from pool or create
			// var spriteSize = _sprites[0].rect;
			// int rows = (int) (field.rect.width / spriteSize.width);
			// var columns = (int) (field.rect.height / spriteSize.height);
			// var offset = new Vector2(spriteSize.width / 2, spriteSize.height / 2);
			// for (int i = 0; i < rows; i++)
			// {
			// 	for (int j = 0; j < columns; j++)
			// 	{
			// 		var cell = GameObject.Instantiate(prefab, field);
			// 		var pos = new Vector2(spriteSize.width * i, spriteSize.height * j);
			// 		cell.transform.localPosition = field.rect.min + pos;
			// 		pool.Add(cell);
			// 	}
			// }
		}
	}
}