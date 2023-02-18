# ML-Test-Project
Some projects that I made to learn ML basics

## Videos:
- [Basic AI](https://www.youtube.com/watch?v=52315AwC7n8)
- [Car AI Fail](https://www.youtube.com/watch?v=GxIa7SrAKA8)

## Setup:
Using Unity 2022.1.8f1

Create a python virtual environment

Apply this commands on virtual environment for Unity ML 2.0

`python -m pip install --upgrade pip`

`pip install torch torchvision torchaudio`

`pip install mlagents`

`pip install protobuf==3.19.6 --upgrade` mlagents don't support new version of protobuff so we downgrade it

Write `mlagents-learn --help` To test install, if lists commands its fine

while training use  `mlagents-learn folderName/settingsFile.yaml(leava blank if you dont have) --run-id=RUNID`

Check [Unity's ML GitHub page](https://github.com/Unity-Technologies/ml-agents) for new ML version installations
