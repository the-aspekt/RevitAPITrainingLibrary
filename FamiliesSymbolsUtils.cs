using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RevitAPITrainingLibrary
{
    public class FamiliesSymbolsUtils
    {
        public static List<FamilySymbol> GetSymbols(Autodesk.Revit.DB.Document doc)
        {
            var familySymbols = new FilteredElementCollector(doc)
                .OfClass(typeof(FamilySymbol))
                .Cast<FamilySymbol>()
                .ToList();

            return familySymbols;
        }

        /// <summary>
        /// Возвращает список совпадающих по названию имен параметров, доступных для редактирования в targetFamilySymbol
        /// </summary>
        /// <param name="basicFamilySymbol"></param>
        /// <param name="targetFamilySymbol"></param>
        /// <returns></returns>
        public static List<string> InteresectSymbolsParameters(FamilySymbol basicFamilySymbol, FamilySymbol targetFamilySymbol)
        {
           ParameterSet firstFamilyParametersSet = basicFamilySymbol.Parameters;
           ParameterSet secondFamilyParameterSet = targetFamilySymbol.Parameters;

           List<string> stringsOfTFSParameters = new List<string>();
           List<string> stringsOfBFSParameters = new List<string>();

           foreach (Parameter item in firstFamilyParametersSet)
           {
              stringsOfTFSParameters.Add(item.Definition.Name);
           }

           foreach (Parameter item in secondFamilyParameterSet)
           {
                if (item.IsReadOnly == false)
                    stringsOfBFSParameters.Add(item.Definition.Name);                
           }

           //получаем список совпадающих в 2-х семействах параметров
           List<string> commonStrings = stringsOfTFSParameters.Intersect(stringsOfBFSParameters).ToList();
            return commonStrings;
        }

        /// <summary>
        /// Получить все FamilySymbol в документе, для которых есть хоть один FamilyInstance
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static List<FamilySymbol> GetUniqueSymbolsInModel(Autodesk.Revit.DB.Document doc)
        {
            List<FamilySymbol> uniqueSymbols = new List<FamilySymbol>();
            List<FamilySymbol> familySymbols = new FilteredElementCollector(doc)
                .OfClass(typeof(FamilySymbol))
                .Cast<FamilySymbol>()
                .ToList();

                foreach (var symbol in familySymbols)
                {
                    var depEl = symbol.GetDependentElements(new ElementClassFilter(typeof(FamilyInstance)));
                    if (depEl.Count > 0)                    
                        uniqueSymbols.Add(symbol);                    
                }            
            return uniqueSymbols;
        }

        /// <summary>
        /// Получить все FamilySymbol в документе, для данного семейства family
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="family"></param>
        /// <returns></returns>
        public static List<FamilySymbol> GetUniqueSymbolsInModel(Autodesk.Revit.DB.Document doc, Family family)
        {
            List<FamilySymbol> uniqueSymbols = new List<FamilySymbol>();

            List<FamilySymbol> familySymbols = new FilteredElementCollector(doc, family.GetDependentElements(new ElementClassFilter(typeof(FamilySymbol))))
               .OfClass(typeof(FamilySymbol))
               .Cast<FamilySymbol>()
               .ToList();

                foreach (var symbol in familySymbols)
                {
                    var depEl = symbol.GetDependentElements(new ElementClassFilter(typeof(FamilyInstance)));
                    if (depEl.Count > 0)
                    {
                        uniqueSymbols.Add(symbol);
                    }
                }
            
            return uniqueSymbols;
        }

        /// <summary>
        /// Связать одноименные параметры двух типоразмеров, если их названия совпадают.
        /// Значение присваивается только если типы данных совпадают, и исходное семейство имеет хоть какое-то значение.
        /// </summary>
        /// <param name="basicFamilySymbol">Семейство, из которого берется значение</param>
        /// <param name="targetFamilySymbol">Семейство, в которое значение вставляется</param>
        /// <param name="lookUpParameterName">Название параметра</param>
        public static void SetParameterValue(FamilySymbol basicFamilySymbol, FamilySymbol targetFamilySymbol, string lookUpParameterName)
        {
            Parameter currentBFSParameter = basicFamilySymbol.LookupParameter(lookUpParameterName);
            Parameter currentTFSParameter = targetFamilySymbol.LookupParameter(lookUpParameterName);
            StorageType thisType = currentBFSParameter.StorageType;

            if (currentBFSParameter.HasValue
            && currentTFSParameter != null
               && currentTFSParameter.AsValueString() != currentBFSParameter.AsValueString()
               && currentBFSParameter.StorageType == currentTFSParameter.StorageType)
            {
                if (thisType == StorageType.Integer)
                {
                    currentTFSParameter.Set(currentBFSParameter.AsInteger());
                }
                else if (thisType == StorageType.Double)
                {
                    currentTFSParameter.Set(currentBFSParameter.AsDouble());
                }
                else if (thisType == StorageType.String)
                {
                    currentTFSParameter.Set(currentBFSParameter.AsString());
                }                
            }
        }
    }
}
