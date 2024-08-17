using Godot;
using System;
using System.IO;
using System.Linq.Expressions;
using System.Runtime;

public partial class Dog : base_npc
{

	bool detected_player = false;
	// PathFollow2D path_follower;
	detector player_detector;
	Vector2 prev_pos;
	float path_speed;

	Vector2 init_position;

	[Export]
	public float chasing_time = 4000f; //4 seconds


	System.Diagnostics.Stopwatch chasing_timer = new System.Diagnostics.Stopwatch();


	int facing = 1;


	public void LookForPlayer()
	{
		if (!detected_player)
		{
			detected_player = player_detector.found_mouse;
			if(detected_player)
			{
				chasing_timer.Start();
			}
		}
		else
		{
			if (chasing_timer.ElapsedMilliseconds > chasing_time)
			{
				player_detector.found_mouse = false;
				detected_player = false;
				chasing_timer.Stop();
				chasing_timer.Reset();
			}
		}
			
		
	}


	public override void move(double delta)
	{
		//do nothing here
		if (detected_player)
		{
			// path_follower.ProgressRatio = 0;
			target = player_detector.player.Position;
			if ((target - Position).Length() < 20)
			{	
				Eat(player_detector.player);
			}

		}
		else
		{
			target = init_position;

		}
		GD.Print($"Dog target: {target}");
		base.move(delta);

		
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		path_speed = .1f;
		
		player_detector = GetNode<detector>("Detector");
		speed = 100;
		init_position = Position;
	}



	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// base._Process(delta);
		setSize();
		// followPath(delta);
		LookForPlayer();
		move(delta);
		die();
		GD.Print($"Dog: {animation_controller.facing}");
		// GD.Print($"Dog found Mouse: {detected_player}");
	}
}
