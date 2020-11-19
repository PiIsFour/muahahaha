using System;
using UnityEngine;

[Serializable]
public struct Foo
{
	public GameObject gameObject;
	public GameObjectHandle handle;
}

public class FooControler : MonoBehaviour
{
	public Foo foo;
}
