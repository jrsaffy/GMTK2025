using Godot;
using System;

public partial class Poop : Node2D
{
	public float RotationSpeed = 15.0f; // Rotation speed in radians per second
	public float poopSpeed = 200;
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	
	public override void _Process(double delta)
	{
		Vector2 velocity = new Vector2(poopSpeed, 0);
		// Apply constant rotation
		Translate(velocity * (float)delta);
		Rotation += RotationSpeed * (float)delta;
	}
}
