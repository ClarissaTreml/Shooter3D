# 3D-Shooter
A 3D Shooter Game using Unity. Implemented an intelligent Agent using ML-Agents from Unity for training.

# Requirements
UnityHub latest version
Unity Editor v2021.3
Visual Studio Code latest version
Python v3.8
Git for Windows

# CMD
cd Shooter3D
python -m venv venv
venv\Scripts\activate
python -m pip install --upgrade pip
pip install mlagents=0.30.0
pip install torch -f https://download.pytorch.org/whl/torch_stable.html
git clone --branch release_20 https://github.com/Unity-Technologies/ml-agents.git
mlagents-learn

# References
[1] https://unity.com/download 
[2] https://github.com/Unity-Technologies/ml-agents/releases 
[3] https://download.pytorch.org/whl/torch_stable.html 
[4] https://git-scm.com/downloads 

