using static Sham.Burnout.BinaryMAP;

namespace Sham.Burnout
{
    public static class Commands_Burnout
    {
        public static void Command_RevMap(string path)
        {
            ParseMAP(path);
        }
    }
}
