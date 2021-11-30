# Air Hockey With Deep Reinforcement Learning

## Summary:
This project is a **single-player Air Hockey game**. The human player (*red*) can play against an AI opponent (*blue*). I trained the AI model using **Deep Reinforcement Learning** with Unity's [ML-Agent](https://github.com/Unity-Technologies/ml-agents/blob/release_18_docs/docs/Readme.md) package. Through a process called **self-play**, the model is able to learn how to play air hockey from scratch by playing it with itself. 

In *AI vs Human* mode, user can control the red player's position by dragging the mouse while pressing the left mouse key. Try to hit the puck into the opponent's goal to score. In *AI vs AI* mode, user can watch how two AI players play against each other. 

## Download:
In the code repository, download Air_Hockey_AI_Opponent.exe. Double click the .exe file and enjoy the air hockey game I made!

## Demo:
In the code repository, download Air_Hockey_AI_Opponent.exe. Double click the .exe file and enjoy the air hockey game I made! Here is a demo of the game:

https://user-images.githubusercontent.com/37155540/143982214-270d0b4f-122f-41d7-8569-2f1c331391ae.mp4

## Training:
The model is trained with ML-Agent package. If you want to learn more about training the model yourself, [this](https://github.com/Unity-Technologies/ml-agents/blob/release_18_docs/docs/Getting-Started.md) tutorial can give you a better understanding of how to train ML model with ML-Agent. Here is the Tensor Board stats of my model's training. Although the training is a bit unstable, the model managed to achieve a high [ELO](https://en.wikipedia.org/wiki/Elo_rating_system) rating, which indicate the model is performing well when playing against itself.  
![AirHockey_LossGraphs](https://user-images.githubusercontent.com/37155540/143984977-901d8c69-56b2-4b1b-99bc-e14d3ec66506.png)
![AirHockey_Entropy](https://user-images.githubusercontent.com/37155540/143984989-c9834f47-e4f8-4ca4-a138-ff59b7fcddec.png)
![AirHockey_ELO](https://user-images.githubusercontent.com/37155540/143984993-a37fa431-8881-4177-b1b9-4438297d26fd.png)


Here are some helpful commands when training your own model with this project, remember to run these commands inside your local ml-agent directory:  

**Training the model with ML-Agent inside ml-agent directory**: mlagents-learn --force config/ppo/AirHockey.yaml --run-id=AirHockey  
**Using Tensor Board to see training progress**: tensorboard --logdir results --port 6006   

Also don't forget to uncomment the corresponding lines inside the Score() function in GameManager.cs script.
