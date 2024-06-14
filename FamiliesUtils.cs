using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingLibrary
{
    public static class FamiliesUtils
    {
        public static List<Family> GetFamilies(Autodesk.Revit.DB.Document doc)
        {

            var familySymbols = new FilteredElementCollector(doc)
                .OfClass(typeof(Family))
                .Cast<Family>()
                .ToList();

            return familySymbols;
        }


    }
}
