using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingLibrary
{
    public class FurnitureUtils
    {
        public static List<FamilySymbol> GetSymbols(Document doc)
        {
            var familySymbols = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Furniture)
                .WhereElementIsElementType()
                .Cast<FamilySymbol>()
                .ToList();

            return familySymbols;
        }
    }
}
