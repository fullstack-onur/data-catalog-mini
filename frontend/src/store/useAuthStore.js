import { create } from "zustand";
import api from "../api/axiosConfig";

export const useAuthStore = create((set, get) => ({
  token: localStorage.getItem("token") || null,
  expiresAt: localStorage.getItem("expiresAt") || null,
  role: localStorage.getItem("role") || null,
  refreshTimer: null,

  login: async (id, password) => {
    const res = await api.post("/auth/login", { id, password });

    const { token, expiresAt, role } = res.data;
    localStorage.setItem("token", token);
    localStorage.setItem("expiresAt", expiresAt);
    localStorage.setItem("role", role);

    set({ token, expiresAt, role });


    get().startRefreshTimer();
  },

  logout: async () => {
    await api.post("/auth/logout");
    localStorage.clear();
    set({ token: null, expiresAt: null, role: null });
    clearInterval(get().refreshTimer);
  },

  refresh: async () => {
    try {
      const res = await api.post("/auth/refresh");
      const { token, expiresAt, role } = res.data;

      localStorage.setItem("token", token);
      localStorage.setItem("expiresAt", expiresAt);
      localStorage.setItem("role", role);

      set({ token, expiresAt, role });
    } catch {
      get().logout();
    }
  },

  startRefreshTimer: () => {
    clearInterval(get().refreshTimer);

    const exp = new Date(get().expiresAt).getTime();
    const now = Date.now();
    const refreshTime = exp - now - 60 * 1000; 

    const timer = setTimeout(() => {
      get().refresh();
    }, refreshTime);

    set({ refreshTimer: timer });
  }
}));
