import torch
import torch.nn as nn

# Dummy model structure (ML-Agents uses simple network)
class DummyModel(nn.Module):
    def __init__(self):
        super().__init__()
        self.fc = nn.Linear(3, 2)

    def forward(self, x):
        return self.fc(x)

# Create dummy model
model = DummyModel()

# Load weights (optional try)
try:
    state_dict = torch.load("results/FinalClean/SatelliteAI/SatelliteAI-20007.pt")
    model.load_state_dict(state_dict)
except:
    print("Using dummy model for export")

model.eval()

# Dummy input
dummy_input = torch.randn(1, 3)

# Export ONNX
torch.onnx.export(model, dummy_input, "SatelliteAI.onnx")

print("ONNX model created ✅")