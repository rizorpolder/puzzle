using System;

public enum ResourceType
{
	Free = 0,
	Soft = 1,
	Hard = 2,
	Restored = 3,
}

[Serializable]
public class Resource : ICloneable
{
	public ResourceType type;
	public int value;

	public Resource()
	{
		type = ResourceType.Soft;
		value = 0;
	}

	public Resource(ResourceType type, int value)
	{
		this.type = type;
		this.value = value;
	}

	public static string GetResourceCountText(Resource resource)
	{
		return "x" + resource.value.ToString();
	}

	protected bool Equals(Resource other)
	{
		return type == other.type && value.Equals(other.value);
	}

	public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((Resource) obj);
	}

	public override int GetHashCode()
	{
		unchecked
		{
			var hashCode = (int) type;
			hashCode = (hashCode * 397) ^ value.GetHashCode();
			return hashCode;
		}
	}

	public object Clone()
	{
		return MemberwiseClone();
	}

	public override string ToString()
	{
		return $"{type}_{value}";
	}
}