using Godot;
using System;

public partial class detector : Area2D
{
	public bool found_mouse = false;
	// Called when the node enters the scene tree for the first time.

	public mouse player;

	void lookForMouse()
	{
		if (!found_mouse)
		{
			Godot.Collections.Array<Node2D> overlapping = GetOverlappingBodies();

			foreach(Node2D body in overlapping)
			{
				if (body.Name == "Mouse")
				{
					found_mouse = true;
					player = (mouse)body;
				}
			}
		}

	}
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		lookForMouse();
	}
}
