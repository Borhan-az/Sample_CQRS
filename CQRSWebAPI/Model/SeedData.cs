using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSWebAPI.Model
{
    public class SeedData
    {
        public List<User> Users { get; } = new List<User>
        {
            new User{ Id=1 , Name = "John" , IsActive = true  },
            new User{ Id=2 , Name = "Tom" , IsActive = true  },
            new User{ Id=3 , Name = "Polly" , IsActive = true  },
            new User{ Id=4 , Name = "Ada" , IsActive = true  },
            new User{ Id=5 , Name = "Amit" , IsActive = true  },
            new User{ Id=6 , Name = "Sarah" , IsActive = false  },
            new User{ Id=7 , Name = "Borhan" , IsActive = true  },
            new User{ Id=8 , Name = "Fin" , IsActive = true  },
            new User{ Id=9 , Name = "Arthur" , IsActive = true  },
        };
    }
}