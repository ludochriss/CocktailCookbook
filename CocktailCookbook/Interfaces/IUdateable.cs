using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailCookbook.Interfaces
{
    interface IUpdateable
    {

        public void Get();

        public void Update();

        public void Delete();

        public void Create();
    }
}
