import { forwardRef, useState } from "react";
import TextField from "@mui/material/TextField";
import InputAdornment from "@mui/material/InputAdornment";
import IconButton from "@mui/material/IconButton";
import Visibility from "@mui/icons-material/Visibility";
import VisibilityOff from "@mui/icons-material/VisibilityOff";
const LoginInput = forwardRef(
  ({ password, fullWidth = false, sx, ...props }, ref) => {
    const [show, setShow] = useState(false);
    const isPassword = Boolean(password);
    return (
      <TextField
        variant="standard"
        fullWidth={fullWidth}
        type={isPassword ? (show ? "text" : "password") : props.type}
        autoComplete={isPassword ? "current-password" : props.autoComplete}
        inputRef={ref}
        slotProps={{
          input: {
            endAdornment: isPassword && (
              <InputAdornment position="end">
                {" "}
                <IconButton
                  aria-label={show ? "Hide Password" : "Show Password"}
                  onClick={() => setShow((s) => !s)}
                  edge="end"
                  tabIndex={-1}
                >
                  {" "}
                  {show ? <VisibilityOff /> : <Visibility />}{" "}
                </IconButton>{" "}
              </InputAdornment>
            ),
          },
        }}
        sx={{
          width: "80%",
          marginBottom: "1rem",
          "& .MuiInput-root": {
            "& .MuiInput-input": { color: "#0F0F0F" },
            "&:before": { borderBottomColor: "rgba(0,0,0,0.25)" },
            "&:hover:not(.Mui-disabled):before": {
              borderBottomColor: "#1A1A1A",
            },
            "&:after": { borderBottomColor: "#1A2A40" },
            "&.Mui-focused:after": { borderBottomColor: "#1A2A40" },
          },
          "& .MuiInputLabel-root": { color: "rgba(0,0,0,0.6)" },
          "& .MuiInputLabel-root.Mui-focused": { color: "#1A2A40" },
          "& .MuiInputLabel-root.MuiInputLabel-shrink": { color: "#1A2A40" },
          ...(sx || {}),
        }}
        {...props}
      />
    );
  },
);
export default LoginInput;
