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
    public static class DefinitionsUtils
    {
        public static Definition FindOrCreateDefinition(ExternalCommandData commandData, string newDefinitionName, CategorySet categorySet, ParameterType parameterType = ParameterType.Text)
        {
            UIApplication uIApplication = commandData.Application;
            Document document = uIApplication.ActiveUIDocument.Document;

            Definition definition = null;
            BindingMap bindingMap = document.ParameterBindings;

            DefinitionBindingMapIterator iterator = bindingMap.ForwardIterator();
            while (iterator.MoveNext())
            {
                if (iterator.Key.Name.Equals(newDefinitionName))
                {
                    definition = iterator.Key;
                    break;
                }                    
            }

            if (definition == null)
            {
                //TaskDialog.Show("Промежуточный итог", "Параметра не существует, создаем новый");

                //Check if shared parameter file was specified
                DefinitionFile definitionFile = uIApplication.Application.OpenSharedParameterFile();
                if (definitionFile == null)
                {
                    TaskDialog.Show("Ошибка", "Не задан файл общих параметров");
                    return definition;
                }
                //Find out if definition alreary exist
                DefinitionGroups definitionGroups = definitionFile.Groups;
                definition = definitionGroups.SelectMany(group => group.Definitions)
                    .FirstOrDefault(def => def.Name.Equals(newDefinitionName));
                if (definition == null)
                {
                    //Find out if group name alreary exist
                    string newGroupName = "newGroup_" + newDefinitionName;
                    if (definitionGroups.ToList().Find(group => group.Name.Equals(newGroupName)) == null)
                        definitionGroups.Create(newGroupName);
                    //create definition
                    DefinitionGroup currentGroup = definitionGroups.get_Item(newGroupName);
                    currentGroup.Definitions.Create(new ExternalDefinitionCreationOptions(newDefinitionName, parameterType));
                    definition = currentGroup.Definitions.get_Item(newDefinitionName);
                    TaskDialog.Show("Промежуточный итог", "Создан новый общий параметр в файле общих параметров " + definition.Name);
                }
                //else
                //    TaskDialog.Show("Промежуточный итог", "Нашелся существующий параметр в файле общих параметров " + definition.Name);
                Binding bindings = uIApplication.Application.Create.NewInstanceBinding(categorySet);
                using (Transaction ts = new Transaction(document, "Добавляем в проект параметр " + definition.Name))
                {
                    ts.Start();
                    bindingMap.Insert(definition, bindings, BuiltInParameterGroup.PG_GENERAL);
                    ts.Commit();
                }
                return definition;
            }
            else
            {
                return definition;
            }
        }

    }
}
