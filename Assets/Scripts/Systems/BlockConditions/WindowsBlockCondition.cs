namespace Systems.BlockConditions
{
	public class WindowsBlockCondition : IBlockCondition
	{
		private WindowsController _controller;
		public WindowsBlockCondition(WindowsController controller)
		{
			_controller = controller;
		}
		public bool Check()
		{
			return _controller.HasActiveWindows;
		}
	}
}