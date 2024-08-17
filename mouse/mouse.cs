using Godot;
using System;

public partial class mouse : CharacterBody2D
{
	
	int speed = 75;
	int base_food = 5;
	int food;

	float animation_scale = 1f;

	AnimationController animation_controller;
	FoodDetector food_detector;
	

	void setSize()
	{
		animation_scale = (float)food / base_food;
		Vector2 one_vector = new Vector2(1f,1f);
		animation_controller.Scale = one_vector * animation_scale;
	}


	public void move()
	{
		Vector2 direction = getDirection();
		Velocity = speed * direction;
		MoveAndSlide();
		
	}

	public void scale()
	{

	}

	void eat()
	{
		// GD.Print($"Food Detected: {food_detector.food_detected}");
		if (Input.IsActionJustPressed("eat"))
		{
			animation_controller.eating = true;
			speed = 0;
			
			if (food_detector.food_detected)
			{
				food++;
				GD.Print($"food: {food}");

				foreach(Node2D body in food_detector.overlapping_boddies)
				{
					if (!(body.Name == "Mouse"))
					{
						if(body is base_npc npc)
						{
							npc.getEaten();
						}
						
					}
				}

			}
		}
		else
		{
			animation_controller.eating = false;
			speed = 75;
		}

		
		
	}

	public Vector2 getDirection()
	{
		int up = Convert.ToInt32(Input.IsActionPressed("up"));
		int down = Convert.ToInt32(Input.IsActionPressed("down"));
		int left = Convert.ToInt32(Input.IsActionPressed("left"));
		int right = Convert.ToInt32(Input.IsActionPressed("right"));

		Vector2 direction = new Vector2(right - left, down - up).Normalized();
		animation_controller.direction = direction;

		return direction;
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	
		animation_controller = GetNode<AnimationController>("AnimationController");
		food_detector = GetNode<FoodDetector>("FoodDetector");
		food = base_food;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		move();
		eat();
		setSize();
	}
}
