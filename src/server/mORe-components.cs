using System.ComponentModel;
using System.Diagnostics;
using System;
using LogicWorld.Server.Circuitry;
using LogicWorld.Server;

namespace mORecomponents {
    public class ROM : LogicComponent {
        private int value { get; set; }
        protected override void DoLogicUpdate() {
            if (value != 1 && base.Inputs[1].On) {
                base.Outputs[0].On = base.Inputs[0].On;
                value = 1;
            }
        }
    }

    public class SevenSegEncoder : LogicComponent {
        protected override void DoLogicUpdate() {
            int[] inputs = new int[4];
            for (int i = 0; i < 4; i++) {
                inputs[i] = base.Inputs[i].On ? 1 : 0;
            }
            int combined = 0;
            for (int i = 0; i < 4; i++) {
                double x = 2;
                double y = i;
                double test = Math.Pow(x, y);
                combined += (int) ((double) inputs[3-i] * test);
            }
            for (int i = 0; i < 7; i++) {
                base.Outputs[i].On = false;
            }
            Log("combined: " + combined + ", inputs: [" + string.Join(", ", inputs) + "]"); 
            if (combined == 0) {
                for (int i = 0; i < 6; i++) {
                    base.Outputs[i].On = true;
                }
            } else if (combined == 1) {
                base.Outputs[1].On = true;
                base.Outputs[2].On = true;
            } else if (combined == 2) {
                base.Outputs[0].On = true;
                base.Outputs[1].On = true;
                base.Outputs[3].On = true;
                base.Outputs[4].On = true;
                base.Outputs[6].On = true;
            } else if (combined == 3) {
                base.Outputs[0].On = true;
                base.Outputs[1].On = true;
                base.Outputs[2].On = true;
                base.Outputs[3].On = true;
                base.Outputs[6].On = true;
            } else if (combined == 4) {
                base.Outputs[1].On = true;
                base.Outputs[2].On = true;
                base.Outputs[5].On = true;
                base.Outputs[6].On = true;
            } else if (combined == 5) {
                base.Outputs[0].On = true;
                base.Outputs[2].On = true;
                base.Outputs[3].On = true;
                base.Outputs[5].On = true;
                base.Outputs[6].On = true;
            } else if (combined == 6) {
                for (int i = 0; i < 7; i++) {
                    base.Outputs[i].On = true;
                }
                base.Outputs[1].On = false;
            } else if (combined == 7) {
                for (int i = 0; i < 3; i++) {
                    base.Outputs[i].On = true;
                }
            } else if (combined == 8) {
                for (int i = 0; i < 7; i++) {
                    base.Outputs[i].On = true;
                }
            } else if (combined == 9) {
                for (int i = 0; i < 3; i++) {
                    base.Outputs[i].On = true;
                }
                base.Outputs[5].On = true;
                base.Outputs[6].On = true;
            } else if (combined == 10) {
                for (int i = 0; i < 7; i++) {
                    base.Outputs[i].On = true;
                }
                base.Outputs[3].On = false;
            } else if (combined == 11) {
                for (int i = 0; i < 7; i++) {
                    base.Outputs[i].On = true;
                }
                base.Outputs[0].On = false;
                base.Outputs[1].On = false;
            } else if (combined == 12) {
                for (int i = 3; i < 5; i++) {
                    base.Outputs[i].On = true;
                }
                base.Outputs[6].On = true;
            } else if (combined == 13) {
                for (int i = 0; i < 7; i++) {
                    base.Outputs[i].On = true;
                }
                base.Outputs[0].On = false;
                base.Outputs[5].On = false;
            } else if (combined == 14) {
                for (int i = 0; i < 7; i++) {
                    base.Outputs[i].On = true;
                }
                base.Outputs[1].On = false;
                base.Outputs[2].On = false;
            } else if (combined == 15) {
                for (int i = 0; i < 7; i++) {
                    base.Outputs[i].On = true;
                }
                base.Outputs[1].On = false;
                base.Outputs[2].On = false;
                base.Outputs[3].On = false;
            }
        }

        private void Log(string info) {
            Logger.Info("[mORecomponents] " + info);
        }
    }

    public class OrGate : LogicComponent {
        protected override void DoLogicUpdate() {
            base.Outputs[0].On = base.Inputs[0].On || base.Inputs[1].On;
        }
    }
    public class XorGate : LogicComponent {
        protected override void DoLogicUpdate() {
            base.Outputs[0].On = base.Inputs[0].On ^ base.Inputs[1].On;
        }
    }

    public class RAM : LogicComponent {

        public bool[] states = { false, false };

        protected override void DoLogicUpdate() {
            bool writevalue = base.Inputs[0].On;
            bool write = base.Inputs[2].On;
            bool address = base.Inputs[1].On;
            int index = write ? 1 : 0;
            states[index] = writevalue;
            base.Outputs[index].On = states[index];
        }
    }
}
