using System;
using System.Reflection;

namespace Entities.Database
{
	class SelectBuilder<T>
	{

		private T mainObject;
		private T newObject;

		public SelectBuilder(T mainObject)
		{
			this.mainObject = mainObject;
		}

		public SelectBuilder<T> Equalize()
		{
			this.newObject = mainObject;
			return this;
		}

		public SelectBuilder<T> Set(string t, object value)
		{
			PropertyInfo field =
				(this.newObject ?? throw new NullReferenceException("Для начала необходимо вызвать Equalize()"))
				.GetType()
				.GetProperty(t);

			(field ?? throw new NullReferenceException("Поле не найдено."))
				.SetValue(this.newObject, value, null);

			return this;
		}

		public T GetInstance()
		{
			return newObject ?? throw new NullReferenceException();
		}
	}
}