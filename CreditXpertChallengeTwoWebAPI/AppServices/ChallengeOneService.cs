using Microsoft.AspNetCore.Mvc;

namespace CreditXpertChallengeWebAPI.AppServices
{
    public class ChallengeOneService
    {
        public ChallengeOneService() { }

        public ShapeColorInfo GetShapeColorInfo() {
            var info = new ShapeColorInfo
            {
                Shapes = new List<string> { "Circle", "Square", "Rectangle" },
                Colors = new List<string> { "Red", "Green", "Blue", "Yellow" }
            };

            return info;
        }

    }

    public class ShapeColorInfo
    {
        public List<string> Shapes { get; set; }
        public List<string> Colors { get; set; }
    }

}