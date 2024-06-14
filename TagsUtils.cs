using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingLibrary
{
    public class TagsUtils
    {
        public static List<FamilySymbol> GetPipeTagTypes(Document doc)
        {
            var familySymbols = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_PipeTags)
                .OfClass(typeof(FamilySymbol))
                .Cast<FamilySymbol>()
                .ToList();
            return familySymbols;

        }
    }
}
