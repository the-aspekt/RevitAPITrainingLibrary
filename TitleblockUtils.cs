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
    public class TitleblockUtils
    {
        public static List<FamilySymbol> GetSymbols(Document doc)
        {
            var familySymbols = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_TitleBlocks)
                .WhereElementIsElementType()
                .Cast<FamilySymbol>()
                .ToList();

            return familySymbols;
        }
        public static List<FamilyInstance> GetInstances(Document doc)
        {
            var familyInstance = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_TitleBlocks)
                .WhereElementIsNotElementType()
                .Cast<FamilyInstance>()
                .ToList();

            return familyInstance;
        }
    }
}
