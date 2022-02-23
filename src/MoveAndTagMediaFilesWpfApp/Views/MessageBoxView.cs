using Microsoft.Toolkit.Mvvm.Messaging;
using MoveAndTagMediaFiles.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveAndTagMediaFilesWpfApp.Views
{
	public class MessageBoxView : IRecipient<UserInformationMessage>, IRecipient<UserWarningMessage>, IRecipient<UserErrorMessage>
	{
		public void Receive(UserInformationMessage message)
		{
			MessageBox.Show(message.Message, message.Title, MessageBoxButton.OK, MessageBoxImage.Information);
		}

		public void Receive(UserWarningMessage message)
		{
			MessageBox.Show(message.Message, message.Title, MessageBoxButton.OK, MessageBoxImage.Warning);
		}

		public void Receive(UserErrorMessage message)
		{
			var msg = message.Message + Environment.NewLine + message.Exception.ToString();
			MessageBox.Show(msg, message.Title, MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}
}
