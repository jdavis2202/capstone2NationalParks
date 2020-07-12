using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    interface ISiteSqlDAO
    {
        IList<Site> GetSiteId(int siteId);
    }
}
