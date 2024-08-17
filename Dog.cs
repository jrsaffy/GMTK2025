using Godot;
using System;
using System.IO;

public partial class Dog : base_npc
{

	

	bool detected_player = false;
	PathFollow2D path_follower;
	Vector2 prev_pos;

	int facing = 1;
	void followPath(double delta)
	{
		if (!detected_player)
		{
			path_follower.ProgressRatio += (float)(speed * delta);
			animation_controller.direction = new Vector2(1f,1f);
			// // Optional: Loop the path
			// if (path_follower.Loop && path_follower.Offset > path_follower.Curve.GetBakedLength())
			// {
			// 	path_follower.Offset = 0;
			// }

		}

	}


	public override void move(double delta)
	{
		//do nothing here
		
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		speed = .1f;
		path_follower = GetParent<PathFollow2D>();
	}



	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// base._Process(delta);
		setSize();
		followPath(delta);
	}
}
