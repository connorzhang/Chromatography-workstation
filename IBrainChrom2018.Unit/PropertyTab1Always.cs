using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms.Design;
using IBrainChrom2018.Properties;

namespace IBrainChrom2018.Unit;

[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
public class PropertyTab1Always : PropertyTab
{
	public override string TabName => "General";

	public override Bitmap Bitmap => new Bitmap(Resources.propertyGeneral);

	public override PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes)
	{
		PropertyDescriptorCollection propertyDescriptorCollection = ((attributes != null) ? TypeDescriptor.GetProperties(component, attributes) : TypeDescriptor.GetProperties(component));
		List<PropertyDescriptor> list = new List<PropertyDescriptor>();
		int num = 0;
		for (int i = 0; i < propertyDescriptorCollection.Count; i++)
		{
			if (propertyDescriptorCollection[i].Attributes[typeof(EditorBrowsableAttribute)] != null && ((EditorBrowsableAttribute)propertyDescriptorCollection[i].Attributes[typeof(EditorBrowsableAttribute)]).State == EditorBrowsableState.Always)
			{
				list.Add(propertyDescriptorCollection[i]);
			}
		}
		PropertyFilter.GetFilter(component.ToString())?.Invoke(component, list);
		return new PropertyDescriptorCollection(list.ToArray());
	}

	public override PropertyDescriptorCollection GetProperties(object component)
	{
		return GetProperties(component, null);
	}
}
