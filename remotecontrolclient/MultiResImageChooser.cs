using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace remotecontrolclient
{
    public class MultiResImageChooserUri
    {
        public Uri BestResolutionImageBG
        {
            get
            {
                switch (ResolutionHelper.CurrentResolution)
                {                        
                    case Resolutions.HD:
                        return new Uri("Assets/bg.screen-720p.jpg", UriKind.Relative);
                    case Resolutions.WXGA:
                        return new Uri("Assets/bg.screen-wxga.jpg", UriKind.Relative);
                    case Resolutions.WVGA:
                        return new Uri("Assets/bg.screen-wvga.jpg", UriKind.Relative);
                    default:
                        return new Uri("Assets/bg.screen-wvga.jpg", UriKind.Relative);
                }
            }
        }

        public Uri BestResolutionImage_Mute_pc
        {
            get
            {
                switch (ResolutionHelper.CurrentResolution)
                {
                    case Resolutions.HD:
                        return new Uri("images/01_mute_pc.screen-wxga.png", UriKind.Relative);
                    case Resolutions.WXGA:
                        return new Uri("images/01_mute_pc.screen-wxga.png", UriKind.Relative);
                    case Resolutions.WVGA:
                        return new Uri("images/01_mute_pc.screen-wvga.png", UriKind.Relative);
                    default:
                        return new Uri("images/01_mute_pc.screen-wvga.png", UriKind.Relative);
                }
            }
        }

    }

}
