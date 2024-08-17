using Godot;
using Godot.Collections;
using System;

public partial class FoodDetector : Area2D
{
	public bool food_detected = false;
	public Godot.Collections.Array<Node2D> overlapping_boddies;



	public Godot.Collections.Array<Node2D> detectFood()
	{	
		bool food_in_area = false;
		var overlappingBodies = (Godot.Collections.Array<Node2D>)GetOverlappingBodies();

		// GD.Print(overlappingBodies);
		
		if (overlappingBodies.Count > 1)
		{
			food_detected = true;
		}
		else
		{
			food_detected = false;
		}

		return overlappingBodies;
	}


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		overlapping_boddies = detectFood();
	}
}
