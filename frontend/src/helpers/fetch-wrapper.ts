import { useAuthStore } from '@/stores/auth.store';

export const fetchWrapper = {
  get: request('GET'),
  post: request('POST'),
  put: request('PUT'),
  delete: request('DELETE')
};

function request(method: string) {
  return async (url: string, body?: object) => {
    const requestOptions: RequestInit = {
      method,
      headers: setHeaders(url, body),
      body: body ? JSON.stringify(body) : null
    };
    const response = await fetch(url, requestOptions);
    return handleResponse(response);
  }
}

function setHeaders(url: String, body?: object): HeadersInit {
  const { token } = useAuthStore();
  const isLoggedIn = token != null && token != "";
  const isApiUrl = url.startsWith(import.meta.env.VITE_API_URL);
  const requestHeaders: HeadersInit = new Headers();
  if (isLoggedIn && isApiUrl) {
    requestHeaders.set("Authorization", `Bearer ${token}`)
  }
  if (body) {
    requestHeaders.set('Content-Type', 'application/json')
  }
  return requestHeaders;
}

async function handleResponse(response: Response) {
  return response.text().then(text => {
    const data = text && JSON.parse(text);
    if (!response.ok) {
      const error = (data && data.message) || response.statusText;
      return Promise.reject(error);
    }
    return data;
  });
}
