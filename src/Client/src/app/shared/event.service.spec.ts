import { EventService } from './event.service';

describe('EventService', () => {
  let service: EventService;

  beforeEach(() => {
    service = new EventService(null);
  });

  test('should be created', () => {
    expect(service).toBeTruthy();
  });
});
