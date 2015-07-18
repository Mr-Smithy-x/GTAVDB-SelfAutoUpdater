using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTAVDB.Listeners
{
    public interface OnNativeListener
    {
        void OnNativesLoaded(string message);
        void OnNativesFailed(string message);
        
    }
}
