using System;
using System.Collections.Generic;
using System.Text;

namespace Nexcel.LRF.Integration
{
    public interface IUrlFactory
    {
        /// <summary>
        /// Generate SSO link
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        string GenerateSsoLink(IRequestSsoModel model);
    }
}
