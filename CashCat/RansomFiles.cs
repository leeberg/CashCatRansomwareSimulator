using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashCat
{
    class RansomFiles
    {

        public string[] fileExtensions = new string[] { ".AngryDuck", ".0day", ".beef", ".beer", ".BitCryptor", ".BOMBO", ".CommonRansom", ".CoronaLock", ".Crypt0L0cker", ".D00mEd", ".Dab", ".Dablio", ".DIABLO6", ".GoldenEye", ".Horriblemorning", ".HUSTONWEHAVEAPROBLEM@KEEMAIL.ME", ".Lazarus", ".locker", ".locky", ".money",".newlock", ".NOOB", ".nuclear", ".parrot",".PayDay", ".petra", ".porno", ".PrOtOnIs", ".prd", ".razor", ".Ryuk", ".satan", ".satana", ".SHARK", ".TROLL", ".weapologize", ".WECANHELP", ".write_us_on_email", ".zeppelin" };

        private Random rand = new Random();

        public string GetRandomFileExtension()
        {
            int index = rand.Next(fileExtensions.Length);
            return fileExtensions[index];
        }


    }
}
