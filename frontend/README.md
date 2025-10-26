# 🎨 Frontend – Data Catalog Mini

This folder contains the **React 19 (Vite)** frontend of the _Data Catalog Mini_ project.  
The goal is to provide a modern, responsive, and modular interface for the Data Governance platform built on top of the .NET 8 Web API backend.

---

## 🚀 Tech Stack

| Category             | Technology           |
| -------------------- | -------------------- |
| Framework            | React 19 + Vite      |
| State Management     | Zustand, React Query |
| Routing              | React Router v7      |
| API Communication    | Axios                |
| Utilities            | clsx, date-fns       |
| Charts               | Recharts             |
| Icons                | Lucide React         |
| Linting & Formatting | ESLint + Prettier    |

---

## 🏗️ Project Structure

```
src/
├── api/ # Axios base instance and endpoint services
├── assets/ # Static assets (images, icons, etc.)
├── components/ # Reusable UI components
├── hooks/ # Custom hooks
├── pages/ # Route-level page components
├── store/ # Zustand global state
├── styles/ # Global and modular CSS files
├── utils/ # Helper functions
├── router.jsx # React Router configuration
├── App.jsx # Root component (Query + Router setup)
├── main.jsx # ReactDOM entry point
└── index.css # Global styles

```

---

## ⚙️ Development

```bash
# Install dependencies
npm install

# Start development server
npm run dev

# Lint and format code
npm run lint
npm run format
```
