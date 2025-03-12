using CommunityToolkit.Mvvm.ComponentModel;
using TP_ConnectFour.Game;

namespace TP_ConnectFour.ViewModels
{
    public partial class TokenViewModel : ObservableObject
    {
        [ObservableProperty]
        private char _color = ' ';

        public TokenViewModel() { }

        public TokenViewModel(Token token)
        {
            Color = token.Color;
        }

        public bool IsEmpty => Color == ' ';
        public bool IsYellow => Color == 'Y';
        public bool IsRed => Color == 'R';
    }
}
