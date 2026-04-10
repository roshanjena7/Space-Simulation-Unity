import torch
import torch.nn as nn

class DummyModel(nn.Module):
    def __init__(self):
        super().__init__()
        self.fc = nn.Linear(3, 2)

    def forward(self, x):
        return self.fc(x)

model = DummyModel()

try:
    state_dict = torch.load("results/FinalClean/SatelliteAI/SatelliteAI-20007.pt")
    model.load_state_dict(state_dict)
except:
    print("Using dummy model")

model.eval()

dummy_input = torch.randn(1, 3)

torch.onnx.export(model, dummy_input, "SatelliteAI.onnx")

print("ONNX created ✅")