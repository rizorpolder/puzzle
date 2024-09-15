using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace External
{
	public class FileUploader : MonoBehaviour
	{
		private void Start()
		{
			DontDestroyOnLoad(gameObject);
		}

		private void FileRequestCallback(string path)
		{
			FileUploaderHelper.SetResult(path);
		}
	}

	public static class FileUploaderHelper
	{
		private static FileUploader fileUploaderObject;
		private static Action<string> pathCallback;

		static FileUploaderHelper()
		{
			var
				methodName =
					"FileRequestCallback";
			var objectName = typeof(FileUploaderHelper).Name;

			var wrapperGameObject = new GameObject(objectName, typeof(FileUploader));
			fileUploaderObject = wrapperGameObject.GetComponent<FileUploader>();

			InitFileLoader(objectName, methodName);
		}

		public static void RequestFile(Action<string> callback, string extensions = ".jpg, .jpeg, .png")
		{
			RequestUserFile(extensions);
			pathCallback = callback;
		}

		public static void SetResult(string path)
		{
			pathCallback.Invoke(path);
			Dispose();
		}

		private static void Dispose()
		{
			ResetFileLoader();
			pathCallback = null;
		}

		[DllImport("__Internal")]
		private static extern void InitFileLoader(string objectName, string methodName);

		[DllImport("__Internal")]
		private static extern void RequestUserFile(string extensions);

		[DllImport("__Internal")]
		private static extern void ResetFileLoader();
	}
}